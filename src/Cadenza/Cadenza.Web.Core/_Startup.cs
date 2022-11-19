﻿using Cadenza.Web.Common.Interfaces.Connections;
using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.Searchbar;
using Cadenza.Web.Common.Interfaces.Startup;
using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Common.Interfaces.Updates;
using Cadenza.Web.Common.Interfaces.View;
using Cadenza.Web.Core.Coordinators;
using Cadenza.Web.Core.Utilities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Cadenza.Web.Core.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Cadenza.Web.Core;

public static class Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddStartupServices()
            .AddUtilities()
            .AddCoordinators()
			.AddInternalServices();

        services
            .AddTransient<IAppStore, Store>()
            .AddTransient<IArtworkFetcher, ArtworkFetcher>()
            .AddTransient<IImageFinder, ImageFinder>()
            .AddTransient<ICurrentTrackStore, CurrentTrackStore>()
            .AddTransient<IItemPlayer, ItemPlayer>()
            .AddTransient<IItemViewer, ItemViewer>()
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddTransient<ISearchSyncService, SearchSyncService>()
            .AddTransient<IUrl, Url>();

        return services;
    }

	private static IServiceCollection AddInternalServices(this IServiceCollection services)
	{
		return services
			.AddTransient<IPlaylistCreator, PlaylistCreator>()
			.AddTransient<ISubTaskRunner, SubTaskRunner>()
			.AddTransient<ITaskProgressUpdater, TaskProgressUpdater>()
			.AddTransient<ITaskRunner, TaskRunner>();
	}

    private static IServiceCollection AddStartupServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IStartupConnectService, StartupConnectService>();
    }

    private static IServiceCollection AddCoordinators(this IServiceCollection services)
    {
        return services
            .AddSingleton<Messenger>()
            .AddSingleton<ConnectionCoordinator>()
            .AddSingleton<PlayCoordinator>()
            .AddSingleton<SearchCoordinator>()
            .AddSingleton<UpdatesCoordinator>()
            .AddSingleton<ViewCoordinator>()
            .AddTransient<IMessenger>(sp => sp.GetRequiredService<Messenger>())
            .AddTransient<IConnectionCoordinator>(sp => sp.GetRequiredService<ConnectionCoordinator>())
            .AddTransient<IPlayCoordinator>(sp => sp.GetRequiredService<PlayCoordinator>())
            .AddTransient<ISearchCoordinator>(sp => sp.GetRequiredService<SearchCoordinator>())
            .AddTransient<IUpdatesCoordinator>(sp => sp.GetRequiredService<UpdatesCoordinator>())
            .AddTransient<IViewCoordinator>(sp => sp.GetRequiredService<ViewCoordinator>())
            .AddTransient<IConnectionService>(sp => sp.GetRequiredService<ConnectionCoordinator>())
            .AddTransient<ISearchCache>(sp => sp.GetRequiredService<SearchCoordinator>());
    }
}
