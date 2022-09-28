namespace Cadenza.Local.SyncService._Startup;

public static class Dependencies
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services
            .AddUtilities()
            .AddUpdaters();

        return services;
    }

    private static IServiceCollection AddUpdaters(this IServiceCollection services)
    {
        return services
            .AddTransient<IService, PlayedFilesService>();
    }
}