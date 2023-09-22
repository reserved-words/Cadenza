using Cadenza.Web.Common.Interfaces.Store;
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
            .AddTransient<IAppStore, Store>()
            .AddTransient<IImageFinder, ImageFinder>()
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IUrl, Url>();

        return services;
    }

    private static IServiceCollection AddStartupServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IStartupTaskService, StartupConnectService>();
    }

    private static IServiceCollection AddCoordinators(this IServiceCollection services)
    {
        return services
            .AddSingleton<Messenger>()
            .AddSingleton<UpdatesCoordinator>()
            .AddTransient<IMessenger>(sp => sp.GetRequiredService<Messenger>())
            .AddTransient<IUpdatesCoordinator>(sp => sp.GetRequiredService<UpdatesCoordinator>());
    }
}
