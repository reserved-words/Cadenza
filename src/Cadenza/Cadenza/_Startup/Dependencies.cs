using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Cadenza.Utilities;
using Cadenza.Interop;
using Cadenza.MudServices;
using Cadenza.Services;
using Cadenza.Interfaces;
using Cadenza.Web.Core;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Core.App;
using Cadenza.Web.Core.Interfaces;
using Cadenza.Web.Core.Utilities;
using Cadenza.Web.LastFM;
using Cadenza.Web.Core.Playlists;
using IDialogService = Cadenza.Interfaces.IDialogService;
using Cadenza.Web.Common.Interop;
using Cadenza.Web.Core.Player;
using Cadenza.Web.Source.Local;
using Cadenza.Web.Database;

namespace Cadenza._Startup;

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
            .AddInteropServices()
            .AddUtilities()
            .AddHttpClient(http)
            .AddLogger(http)
            .AddAppServices()
            .AddUIHelpers()
            .AddTimers()
            .AddAPIWrapper()
            .AddLastFm(builder.Configuration, "LastFmApi")
            .AddSources(builder.Configuration)
            .AddDatabase(builder.Configuration, "DatabaseApi");

        builder.Services
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IItemPlayer, ItemPlayer>()
            .AddTransient<IItemViewer, ItemViewer>();

        builder.Services
            .AddTransient<IPlayer, CorePlayer>()
            .AddTransient<IUtilityPlayer, TimingPlayer>()
            .AddTransient<IUtilityPlayer, TrackingPlayer>();

        return builder;
    }

    private static IServiceCollection AddInteropServices(this IServiceCollection services)
    {
        return services.AddTransient<INavigation, NavigationInterop>()
            .AddTransient<IStoreGetter, StoreInterop>()
            .AddTransient<IStoreSetter, StoreInterop>();
    }

    private static IServiceCollection AddStartupServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IStartupConnectService, StartupConnectService>();
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<AppService>()
            .AddSingleton<ItemUpdatesHandler>()
            .AddTransient<IAppConsumer>(sp => sp.GetRequiredService<AppService>())
            .AddTransient<IAppController>(sp => sp.GetRequiredService<AppService>())
            .AddTransient<IUpdatesConsumer>(sp => sp.GetRequiredService<ItemUpdatesHandler>())
            .AddTransient<IUpdatesController>(sp => sp.GetRequiredService<ItemUpdatesHandler>());
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
            .AddTransient<INotificationService, MudNotificationService>();
    }

    private static IServiceCollection AddTimers(this IServiceCollection services)
    {
        return services
            .AddSingleton<TrackTimer>()
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackProgressedConsumer>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackFinishedConsumer>(sp => sp.GetRequiredService<TrackTimer>());
    }

    private static IServiceCollection AddSources(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddLocalSource<HtmlPlayer>(config, "LocalApi")
            .AddTransient<IArtworkFetcher, CoreArtworkFetcher>();
    }

    public static IServiceCollection AddAPIWrapper(this IServiceCollection services)
    {
        return services
            .AddTransient<IUrl, Url>();
    }
}