namespace Cadenza.Local.SyncService._Startup;

public static class Dependencies
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services
            .AddUtilities()
            .AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IService, PlayedFilesService>();
    }
}