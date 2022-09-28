namespace Cadenza.SyncService._Startup;

public static class Dependencies
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services
            .AddTransient(sp => new HttpClient())
            .AddUtilities()
            .AddInternals()
            .AddServices();

        return services;
    }

    private static IServiceCollection AddInternals(this IServiceCollection services)
    {
        return services
            .AddTransient<IDatabaseRepository, DatabaseRepository>()
            .AddTransient<ISourceRepository, LocalRepository>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IService, AddedTracksHandler>()
            .AddTransient<IService, RemovedTracksHandler>()
            .AddTransient<IService, UpdatesHandler>();
    }
}