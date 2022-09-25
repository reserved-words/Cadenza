using Cadenza.Local.SyncService.Updaters;

namespace Cadenza.Local.SyncService._Startup;

public static class Dependencies
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services
           .AddUpdaters()
           .AddFileAccess();

        return services;
    }

    private static IServiceCollection AddUpdaters(this IServiceCollection services)
    {
        return services
            .AddTransient<IUpdateService, PlayedFilesHandler>();
    }
}