using Cadenza.SyncService.Settings;

namespace Cadenza.SyncService._Startup
{
    public static class Configuration
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services)
        {
            var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
                ?? "appsettings.json";

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(settingsPath, false)
                .Build();

            services.AddSingleton(configuration);

            services
                .ConfigureDatabaseApi(configuration, "DatabaseApi")
                .ConfigureLocalApi(configuration, "LocalApi");

            return services;
        }

        private static IServiceCollection ConfigureDatabaseApi(this IServiceCollection services, IConfiguration config, string path)
        {
            var configSection = config.GetSection(path);
            services.Configure<DatabaseApiSettings>(configSection);
            return services;
        }

        private static IServiceCollection ConfigureLocalApi(this IServiceCollection services, IConfiguration config, string path)
        {
            var configSection = config.GetSection(path);
            services.Configure<LocalApiSettings>(configSection);
            return services;
        }
    }
}
