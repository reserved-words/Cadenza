using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Cadenza.Database;
using Cadenza.Source.Local;
using Cadenza.Source.Spotify;
using Cadenza.Core;
using Cadenza.Common;
using Cadenza.Utilities;

namespace Cadenza;

public static class Services
{
    public static WebAssemblyHostBuilder RegisterDependencies(this WebAssemblyHostBuilder builder)
    {
        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddUtilities()
            .AddHttpClient(http)
            .AddLogger(http)
            .AddMudServices()
            .AddAppServices()
            .AddUIHelpers()
            .AddTimers()
            .AddLastFm()
            .AddSources()
            .AddPlayers()
            .AddSourceFactories()
            .AddSingletons()
            .AddCacheRepositories()
            .AddDatabaseRepositories();

        builder.Services
            .AddTransient<ISyncService, SyncService>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IPlaylistPlayer, PlaylistPlayer>();

        return builder;
    }

    private static IServiceCollection AddCacheRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<ITrackRepository, Core.TrackRepository>();
    }

    public static IServiceCollection AddSingletons(this IServiceCollection services)
    {
        return services.AddSingleton<TrackTimer>()
            .AddSingleton<IPlayer>(sp => new TrackingPlayer(
                sp.GetRequiredService<TimingPlayer>(),
                sp.GetRequiredService<IPlayTracker>()));
//            .AddSingleton<PlayerLibrary>();
        ;
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services.AddSingleton<AppService>()
            .AddTransient<IAppConsumer>(sp => sp.GetRequiredService<AppService>())
            .AddTransient<IAppController>(sp => sp.GetRequiredService<AppService>());
    }

    private static IServiceCollection AddUIHelpers(this IServiceCollection services)
    {
        return services
            .AddTransient<IDialogService, MudDialogService>()
            .AddTransient<IProgressDialogService, ProgressDialogService>()
            .AddTransient<INotificationService, MudNotificationService>()
            .AddTransient<IStoreGetter, Store>()
            .AddTransient<IStoreSetter, Store>();
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
            .AddTransient<IFavouritesController, LastFmService>()
            .AddTransient<IHistory, LastFmService>();
    }

    private static IServiceCollection AddSources(this IServiceCollection services)
    {
        return services
            .AddSpotifySource<SpotifyConfig>()
            .AddLocalSource<HtmlPlayer>();
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
            .AddLocalSource<HtmlPlayer>();
    }

    //private static IServiceCollection AddLibraries(this IServiceCollection services)
    //{
    //    return services
    //        .AddTransient<ILibraryConsumer>(sp => sp.GetRequiredService<PlayerLibrary>())
    //        .AddTransient<ILibraryController>(sp => sp.GetRequiredService<PlayerLibrary>());
    //}

    private static IServiceCollection AddSourceFactories(this IServiceCollection services)
    {
        return services
            .AddTransient(sp => GetPlayers(sp))
            //.AddTransient(sp => sp.GetUpdaters())
            .AddTransient(sp => GetOverriders(sp));
    }

    private static Dictionary<LibrarySource, IAudioPlayer> GetPlayers(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IAudioPlayer>
        {
            { LibrarySource.Local, sp.GetLocalPlayer() },
            { LibrarySource.Spotify, sp.GetSpotifyPlayer() }
        };
    }

    //private static Dictionary<LibrarySource, IUpdater> GetUpdaters(this IServiceProvider sp)
    //{
    //    return new Dictionary<LibrarySource, IUpdater>
    //    {
    //        { LibrarySource.Local, sp.GetLocalUpdater() },
    //        { LibrarySource.Spotify, sp.GetSpotifyUpdater() }
    //    };
    //}

    private static Dictionary<LibrarySource, IOverridesService> GetOverriders(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IOverridesService>
        {
            { LibrarySource.Spotify, sp.GetSpotifyOverrider() }
        };
    }
}