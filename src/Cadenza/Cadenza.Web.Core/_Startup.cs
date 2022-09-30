using Cadenza.Web.Core.Players;
using Cadenza.Web.Core.Utilities;

namespace Cadenza.Web.Core;

public static class Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ISearchRepositoryCache, SearchRepositoryCache>()
            .AddTransient<ISearchSyncService, SearchSyncService>();

        services
            .AddSingleton<ConnectorService>()
            .AddTransient<IConnectorConsumer>(sp => sp.GetRequiredService<ConnectorService>())
            .AddTransient<IConnectorController>(sp => sp.GetRequiredService<ConnectorService>())
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddStartupServices()
            .AddTransient<IArtworkFetcher, ArtworkFetcher>()
            .AddUtilities()
            .AddAppServices()
            .AddTimers()
            .AddAPIWrapper()
            .AddTransient<IAppStore, Store>();

        services
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IItemPlayer, ItemPlayer>()
            .AddTransient<IItemViewer, ItemViewer>();

        services
            .AddTransient<IPlayer, CorePlayer>()
            .AddTransient<IUtilityPlayer, TimingPlayer>()
            .AddTransient<IUtilityPlayer, TrackingPlayer>();

        return services;
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

    private static IServiceCollection AddTimers(this IServiceCollection services)
    {
        return services
            .AddSingleton<TrackTimer>()
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackProgressedConsumer>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackFinishedConsumer>(sp => sp.GetRequiredService<TrackTimer>());
    }

    private static IServiceCollection AddAPIWrapper(this IServiceCollection services)
    {
        return services
            .AddTransient<IUrl, Url>();
    }
}
