using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Cadenza.Database;
using Cadenza.Source.Local;
using Cadenza.Source.Spotify;
using Cadenza.Core;
using Cadenza.Common;
using Cadenza.Utilities;
using Cadenza.LastFM;
using Cadenza.API.Wrapper;

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
            .AddAppServices()
            .AddUIHelpers()
            .AddTimers()
            .AddLastFm()
            .AddSources()
            .AddSourceFactories()
            .AddCacheRepositories()
            .AddDatabaseRepositories();

        builder.Services
            .AddTransient<ISyncService, SyncService>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IPlaylistPlayer, PlaylistPlayer>();

        builder.Services
            .AddTransient<IPlayer, CorePlayer>()
            .AddTransient<IUtilityPlayer, TimingPlayer>()
            .AddTransient<IUtilityPlayer, TrackingPlayer>();

        return builder;
    }

    private static IServiceCollection AddCacheRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<ITrackRepository, Core.TrackRepository>();
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
            .AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            })
            .AddTransient<IDialogService, MudDialogService>()
            .AddTransient<IProgressDialogService, ProgressDialogService>()
            .AddTransient<INotificationService, MudNotificationService>()
            .AddTransient<IStoreGetter, Store>()
            .AddTransient<IStoreSetter, Store>();
    }

    private static IServiceCollection AddTimers(this IServiceCollection services)
    {
        return services
            .AddSingleton<TrackTimer>()
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackProgressedConsumer>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackFinishedConsumer>(sp => sp.GetRequiredService<TrackTimer>());
    }

    private static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        return services.AddTransient<IPlayTracker, LastFmService>()
            .AddTransient<IFavouritesConsumer, LastFmService>()
            .AddTransient<IFavouritesController, LastFmService>()
            .AddTransient<ILastFmStartup, LastFmStartup>()
            .AddLastFMCore();
    }

    private static IServiceCollection AddSources(this IServiceCollection services)
    {
        return services
            .AddSpotifySource<SpotifyConfig>()
            .AddSpotifyCore()
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
            //.AddTransient(sp => sp.GetUpdaters())
            .AddTransient(sp => GetOverriders(sp));
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