using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Cadenza.Database;
using Cadenza.Source.Local;
using Cadenza.Source.Spotify;
using Cadenza.Common;
using Cadenza.Utilities;
using Cadenza.LastFM;
using Cadenza.API.Wrapper.Spotify;
using Cadenza.API.Wrapper.LastFM;
using Cadenza.API.Wrapper.Core;
using Cadenza.Library;
using Cadenza.Source.Spotify.Player;

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
        return services.AddTransient<IPlayTracker, LastFmService>()
            .AddTransient<IFavouritesConsumer, LastFmService>()
            .AddTransient<IFavouritesController, LastFmService>()
            .AddLastFMCore();
    }

    private static IServiceCollection AddSources(this IServiceCollection services)
    {
        return services
            .AddSpotifySource<SpotifyConfig>()
            .AddSpotifyCore()
            .AddLocalSource<HtmlPlayer>()
            .AddMergedRepositories();
    }

    private static IServiceCollection AddSpotifySource<TConfig>(this IServiceCollection services) where TConfig : class, ISpotifyApiConfig
    {
        return services
            .AddTransient<ISpotifyApiConfig, TConfig>()
            .AddTransient<ISourcePlayer, SpotifyPlayer>()
            .AddTransient<IApiHelper, ApiHelper>()
            .AddTransient<IPlayerApi, PlayerApi>()
            .AddTransient<ISpotifyInterop, SpotifyInterop>();
    }

    private static IAudioPlayer GetSpotifyPlayer(this IServiceProvider services)
    {
        return services.GetRequiredService<SpotifyPlayer>();
    }
}