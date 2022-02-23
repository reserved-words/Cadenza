using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Cadenza.Source.Local;
using Cadenza.Utilities;
using Cadenza.LastFM;
using Cadenza.Library;
using Cadenza.Source.Spotify.Player;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Player;
using Cadenza.Core.App;
using Cadenza.Core.Utilities;
using Cadenza.Core.Playlists;
using Cadenza.Source.Spotify;

namespace Cadenza;

public static class Dependencies
{
    public static WebAssemblyHostBuilder RegisterDependencies(this WebAssemblyHostBuilder builder)
    {
        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddSingleton<SearchRepositoryCache>()
            .AddTransient<ISearchSyncService, SearchSyncService>();

        builder.Services
            .AddSingleton<ConnectorService>()
            .AddTransient<IConnectorConsumer>(sp => sp.GetRequiredService<ConnectorService>())
            .AddTransient<IConnectorController>(sp => sp.GetRequiredService<ConnectorService>())
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddStartupServices()
            .AddUtilities()
            .AddHttpClient(http)
            .AddLogger(http)
            .AddAppServices()
            .AddUIHelpers()
            .AddTimers()
            .AddAPIWrapper()
            .AddLastFm()
            .AddSources();

        builder.Services
            .AddTransient<IStartupSyncService, StartupSyncService>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IItemPlayer, ItemPlayer>()
            .AddTransient<IItemViewer, ItemViewer>();

        builder.Services
            .AddTransient<IPlayer, CorePlayer>()
            .AddTransient<IUtilityPlayer, TimingPlayer>()
            .AddTransient<IUtilityPlayer, TrackingPlayer>();

        return builder;
    }

    private static IServiceCollection AddStartupServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IStartupConnectService, StartupConnectService>()
            .AddTransient<IConnectionTaskBuilder, ApiConnectionTaskBuilder>()
            .AddTransient<IConnectionTaskBuilder, LastFmConnectionTaskBuilder>()
            .AddTransient<IConnectionTaskBuilder, LocalLibraryConnectionTaskBuilder>()
            .AddTransient<IConnectionTaskBuilder, SpotifyConnectionTaskBuilder>();
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
        return services.AddTransient<IPlayTracker, Scrobbler>()
            .AddTransient<IFavouritesConsumer, Favourites>()
            .AddTransient<IFavouritesController, Favourites>()
            .AddLastFMCore();
    }

    private static IServiceCollection AddSources(this IServiceCollection services)
    {
        return services
            .AddSpotifySource()
            .AddSpotifyCore()
            .AddLocalSource<HtmlPlayer>()
            .AddMergedRepositories();
    }

    private static IServiceCollection AddSpotifySource(this IServiceCollection services)
    {
        return services
            .AddTransient<IApiHelper, ApiHelper>()
            .AddTransient<IDeviceHelper, DeviceHelper>()
            .AddTransient<IDevicesApi, DevicesApi>()
            .AddTransient<IPlayerApi, PlayerApi>()
            .AddTransient<IProgressApi, ProgressApi>()
            .AddTransient<ISpotifyAuthHelper, SpotifyAuthHelper>()
            .AddTransient<ISpotifyInterop, SpotifyInterop>()
            .AddTransient<ISourcePlayer, SpotifyPlayer>();
    }

    public static IServiceCollection ConfigureCoreAPI(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<CoreApiSettings>(section);
    }

    public static IServiceCollection AddAPIWrapper(this IServiceCollection services)
    {
        return services
            .AddTransient<IUrl, Url>();
    }
}