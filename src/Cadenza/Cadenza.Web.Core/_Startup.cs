using Cadenza.Web.Common.Interfaces.Connections;
using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.Searchbar;
using Cadenza.Web.Common.Interfaces.Startup;
using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Common.Interfaces.Updates;
using Cadenza.Web.Common.Interfaces.View;
using Cadenza.Web.Core.Coordinators;
using Cadenza.Web.Core.Utilities;

namespace Cadenza.Web.Core;

public static class Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddStartupServices()
            .AddUtilities()
            .AddCoordinators();

        services
            .AddTransient<ISearchSyncService, SearchSyncService>()
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddTransient<IArtworkFetcher, ArtworkFetcher>()
            .AddTransient<IAppStore, Store>()
            .AddTransient<ICurrentTrackStore, CurrentTrackStore>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IItemPlayer, ItemPlayer>()
            .AddTransient<IItemViewer, ItemViewer>()
            .AddTransient<IUrl, Url>();

        return services;
    }

    private static IServiceCollection AddStartupServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IStartupConnectService, StartupConnectService>();
    }

    private static IServiceCollection AddCoordinators(this IServiceCollection services)
    {
        return services
            .AddCoordinator<ConnectionCoordinator, IConnectionController, IConnectionMessenger>()
            .AddCoordinator<PlayCoordinator, IPlayController, IPlayMessenger>()
            .AddCoordinator<SearchCoordinator, ISearchController, ISearchMessenger>()
            .AddCoordinator<UpdatesCoordinator, IUpdatesController, IUpdatesMessenger>()
            .AddCoordinator<ViewCoordinator, IViewController, IViewMessenger>()
            .AddTransient<IConnectionService>(sp => sp.GetRequiredService<ConnectionCoordinator>())
            .AddTransient<ISearchCache>(sp => sp.GetRequiredService<SearchCoordinator>());
    }

    private static IServiceCollection AddCoordinator<TCoordinator, IController, IMessenger>(this IServiceCollection services) 
        where TCoordinator : class, IController, IMessenger
        where IController : class
        where IMessenger : class
    {
        return services.AddSingleton<TCoordinator>()
            .AddTransient<IController>(sp => sp.GetRequiredService<TCoordinator>())
            .AddTransient<IMessenger>(sp => sp.GetRequiredService<TCoordinator>());
    }
}
