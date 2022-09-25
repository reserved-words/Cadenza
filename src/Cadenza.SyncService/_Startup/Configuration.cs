using Cadenza.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .ConfigureLogger(configuration, "Logging");

            return services;
        }
    }
}
