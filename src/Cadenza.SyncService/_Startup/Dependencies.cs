namespace Cadenza.SyncService._Startup;

public static class Dependencies
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services
            .AddTransient(sp => new HttpClient())
            .AddRepositories()
            .AddUtilities()
            .AddUpdaters();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IDatabaseRepository, DatabaseRepository>()
            .AddTransient<ISourceRepository, LocalRepository>();
    }

    private static IServiceCollection AddUpdaters(this IServiceCollection services)
    {
        return services
            .AddTransient<IUpdateService, AddedTracksHandler>()
            .AddTransient<IUpdateService, RemovedTracksHandler>()
            .AddTransient<IUpdateService, UpdatesHandler>();
    }
}