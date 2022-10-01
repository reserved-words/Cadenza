using Cadenza.Web.Common.Interfaces.Coordinators;
using Cadenza.Web.Core.Coordinators;
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
            .AddSingleton<ConnectionCoordinator>()
            .AddTransient<IConnectorConsumer>(sp => sp.GetRequiredService<ConnectionCoordinator>())
            .AddTransient<IConnectorController>(sp => sp.GetRequiredService<ConnectionCoordinator>())
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddStartupServices()
            .AddTransient<IArtworkFetcher, ArtworkFetcher>()
            .AddUtilities()
            .AddAppServices()
            .AddAPIWrapper()
            .AddTransient<IAppStore, Store>()
            .AddTransient<ICurrentTrackStore, CurrentTrackStore>();

        services
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IItemPlayer, ItemPlayer>()
            .AddTransient<IItemViewer, ItemViewer>();

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
            .AddSingleton<AppCoordinator>()
            .AddSingleton<UpdatesCoordinator>()
            .AddTransient<IAppConsumer>(sp => sp.GetRequiredService<AppCoordinator>())
            .AddTransient<IAppController>(sp => sp.GetRequiredService<AppCoordinator>())
            .AddTransient<IUpdatesConsumer>(sp => sp.GetRequiredService<UpdatesCoordinator>())
            .AddTransient<IUpdatesController>(sp => sp.GetRequiredService<UpdatesCoordinator>());
    }

    private static IServiceCollection AddAPIWrapper(this IServiceCollection services)
    {
        return services
            .AddTransient<IUrl, Url>();
    }
}
