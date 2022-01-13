using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadenza.Local.SyncService
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

            services.AddSingleton<IConfiguration>(configuration);

            services
                .ConfigureLogger(configuration, "Logging")
                .ConfigureLibraryLocation(configuration, "LibraryPaths")
                .ConfigurePlayLocation(configuration, "CurrentlyPlaying")
                .ConfigureMusicLocation(configuration, "MusicLibrary");

            return services;
        }
    }
}
