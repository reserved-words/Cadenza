using Cadenza.Database;
using IndexedDB.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Cadenza._Config;
using Cadenza.Library;
using Cadenza.Source.Local;
using Cadenza.Source.Spotify;


namespace Cadenza;

public static class Services
{
    public static WebAssemblyHostBuilder RegisterDependencies(this WebAssemblyHostBuilder builder)
    {
        var http = new System.Net.Http.HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddCommonUtilities()
            .AddHttpClient(http)
            .AddLogger(http)
            .AddMudServices()
            .AddAppServices()
            .AddUtilities()
            .AddTimers()
            .AddLastFm()
            .AddSpotify()
            .AddLocalLibrary()
            .AddLibraries()
            .AddPlayers()
            .AddSourceFactories()
            .AddSingletons();

        builder.Services.AddTransient<IPlayerApiUrl, PlayerApiConfig>();

        builder.Services.AddSingleton<IIndexedDbFactory, IndexedDbFactory>();
        builder.Services.AddTransient<IMainRepository, MainRepository>();
        builder.Services.AddTransient<IArtistRepository, ArtistRepository>();
        builder.Services.AddTransient<ITrackRepositoryUpdater, Database.TrackRepository>();
        builder.Services.AddTransient<ITrackRepository, Player.TrackRepository>();
        builder.Services.AddTransient<IPlayTrackRepositoryUpdater, Database.PlayTrackRepository>();
        builder.Services.AddTransient<IPlayTrackRepository, Player.PlayTrackRepository>();

        builder.Services.AddTransient<IStartupSyncService, StartupSyncService>();

        return builder;
    }

    public static IServiceCollection AddSingletons(this IServiceCollection services)
    {
        var mainCache = new Cache();

        services
            .AddTransient<ICombinedSourceLibraryUpdater>(sp => new CombinedSourceLibraryUpdater(
                sp.GetUpdaters(),
                sp.GetRequiredService<IMerger>(),
                mainCache));

        return services.AddSingleton<TrackTimer>()
            .AddSingleton<IPlayer>(sp => new TrackingPlayer(
                sp.GetRequiredService<TimingPlayer>(),
                sp.GetRequiredService<IPlayTracker>()))
            .AddSingleton<LocalPlayer>()
            .AddSingleton<PlayerLibrary>();
        ;
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services.AddSingleton<AppService>()
            .AddTransient<IAppConsumer>(sp => sp.GetRequiredService<AppService>())
            .AddTransient<IAppController>(sp => sp.GetRequiredService<AppService>());
    }

    private static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IDialogService, MudDialogService>()
            .AddTransient<IProgressDialogService, ProgressDialogService>()
            .AddTransient<INotificationService, MudNotificationService>()
            .AddTransient<IStoreGetter, Store>()
            .AddTransient<IStoreSetter, Store>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>();
    }

    private static IServiceCollection AddTimers(this IServiceCollection services)
    {
        return services
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackProgressedConsumer>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackFinishedConsumer>(sp => sp.GetRequiredService<TrackTimer>());
    }

    private static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        return services.AddTransient<IPlayTracker, LastFmService>()
            .AddTransient<IFavouritesConsumer, LastFmService>()
            .AddTransient<IFavouritesController, LastFmService>();
    }

    private static IServiceCollection AddSpotify(this IServiceCollection services)
    {
        return services
            .AddSpotifySource()
            .AddTransient<ISpotifyApiConfig, SpotifyConfig>();
    }

    private static IServiceCollection AddPlayers(this IServiceCollection services)
    {
        return services.AddTransient<CorePlayer>()
            .AddTransient<TimingPlayer>(sp => new TimingPlayer(
                sp.GetRequiredService<CorePlayer>(),
                sp.GetRequiredService<ITrackTimerController>()));
    }

    private static IServiceCollection AddLocalLibrary(this IServiceCollection services)
    {
        return services
            .AddTransient<LocalLibraryUpdater>()
            .AddTransient<IAudioPlayer, HtmlPlayer>()
            .AddTransient<IFileUpdateQueue, LocalLibraryUpdater>()
            .AddTransient<ILocalApiConfig, LocalApiConfig>();
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        return services
            .AddTransient<LocalLibrary>()
            .AddTransient<ILibraryConsumer>(sp => sp.GetRequiredService<PlayerLibrary>())
            .AddTransient<ILibraryController>(sp => sp.GetRequiredService<PlayerLibrary>());
    }

    private static IServiceCollection AddSourceFactories(this IServiceCollection services)
    {
        return services
            .AddTransient(sp => GetPlayers(sp))
            .AddTransient(sp => sp.GetUpdaters())
            .AddTransient(sp => GetOverriders(sp))
            .AddTransient(sp => GetSourceRepositories(sp));
    }

    private static Dictionary<LibrarySource, ISourceRepository> GetSourceRepositories(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, ISourceRepository>
        {
            { LibrarySource.Local, sp.GetService<LocalLibrary>() },
            { LibrarySource.Spotify, sp.GetSpotifyRepository() }
        };
    }

    private static Dictionary<LibrarySource, IAudioPlayer> GetPlayers(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IAudioPlayer>
        {
            { LibrarySource.Local, sp.GetService<LocalPlayer>() },
            { LibrarySource.Spotify, sp.GetSpotifyPlayer() }
        };
    }

    private static Dictionary<LibrarySource, ISourceLibraryUpdater> GetUpdaters(this IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, ISourceLibraryUpdater>
        {
            { LibrarySource.Local, sp.GetService<LocalLibraryUpdater>() },
            { LibrarySource.Spotify, sp.GetSpotifyUpdater() }
        };
    }

    private static Dictionary<LibrarySource, IOverridesService> GetOverriders(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IOverridesService>
        {
            { LibrarySource.Spotify, sp.GetSpotifyOverrider() }
        };
    }
}