using Cadenza.Database;
using IndexedDB.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ILogger = Cadenza.Common.ILogger;
using Cadenza._Config;
using Cadenza.Library;
using Cadenza.Source.Local;
using Cadenza.Source.Spotify;
using HttpClient = Cadenza.Common.HttpClient;


namespace Cadenza;

public static class DependencyInjection
{
    public static WebAssemblyHostBuilder RegisterDependencies(this WebAssemblyHostBuilder builder)
    {
        var http = new System.Net.Http.HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddTransient(sp => http)
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

        builder.Services.AddTransient<ILogger, Logger>();

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
        var spotifyCache = new Cache();
        var mainCache = new Cache();

        services.AddTransient<ILongRunningTaskService, LongRunningTaskService>();

        services
            .AddTransient<ICombinedSourceLibraryUpdater>(sp => new CombinedSourceLibraryUpdater(
                sp.GetUpdaters(),
                sp.GetRequiredService<IMerger>(),
                mainCache))
            .AddTransient<SpotifyLibrary>(sp =>
            {
                var merger = sp.GetRequiredService<IMerger>();
                var staticSources = new List<IStaticSource> {
                        sp.GetService<SpotifyOverrides>(),
                        sp.GetService<SpotifyApiLibrary>()
                };
                var combinedLibrary = new CombinedStaticLibrary(spotifyCache, merger, staticSources);
                return new SpotifyLibrary(
                    combinedLibrary,
                    sp.GetService<ISpotifyLibraryApi>(),
                    sp.GetService<IIdGenerator>());
            })
            .AddTransient<SpotifyUpdater>(sp => new SpotifyUpdater(
                new LibraryUpdater(sp.GetRequiredService<IMerger>(), spotifyCache),
                sp.GetRequiredService<IOverridesService>()
                ));

        return services.AddSingleton<TrackTimer>()
            .AddSingleton<SpotifyPlayer>()
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
            .AddTransient<IDateTime, CurrentDateTime>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IHttpClient, HttpClient>()
            .AddTransient<IIdGenerator, IdGenerator>()
            .AddTransient<IMerger, Merger>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<INotificationService, MudNotificationService>()
            .AddTransient<IRandomGenerator, RandomGenerator>()
            .AddTransient<IStoreGetter, Store>()
            .AddTransient<IStoreSetter, Store>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IShuffler, Shuffler>()
            .AddTransient<IValueMerger, ValueMerger>();
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
        return services.AddTransient<ISpotifyApiConfig, SpotifyConfig>()
            .AddTransient<ISpotifyApi, SpotifyApi>()
            .AddTransient<ISpotifyLibraryApi, SpotifyLibraryApi>()
            .AddTransient<ISpotifyPlayerApi, SpotifyPlayerApi>()
            .AddTransient<IOverridesService, SpotifyOverridesService>()
            .AddTransient<SpotifyOverrides>()
            .AddTransient<SpotifyApiLibrary>();
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
            { LibrarySource.Spotify, sp.GetService<SpotifyLibrary>() }
        };
    }

    private static Dictionary<LibrarySource, IAudioPlayer> GetPlayers(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IAudioPlayer>
        {
            { LibrarySource.Local, sp.GetService<LocalPlayer>() },
            { LibrarySource.Spotify, sp.GetService<SpotifyPlayer>() }
        };
    }

    private static Dictionary<LibrarySource, ISourceLibraryUpdater> GetUpdaters(this IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, ISourceLibraryUpdater>
        {
            { LibrarySource.Local, sp.GetService<LocalLibraryUpdater>() },
            { LibrarySource.Spotify, sp.GetService<SpotifyUpdater>() }
        };
    }

    private static Dictionary<LibrarySource, IOverridesService> GetOverriders(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IOverridesService>
        {
            { LibrarySource.Spotify, sp.GetService<SpotifyOverridesService>() }
        };
    }
}