namespace Cadenza.SyncService._Startup
{
    public static class Configuration
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services)
        {
            var configuration = services.RegisterJson();

            return services
                .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
                .ConfigureSettings<DatabaseApiSettings>(configuration, "DatabaseApi")
                .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi");
        }
    }
}
