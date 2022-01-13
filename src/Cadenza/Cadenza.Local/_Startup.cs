using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Local
{
    public static class Startup
    {
        public static IServiceCollection ConfigureLibraryLocation(this IServiceCollection services, IConfiguration config, params string[] sections)
        {
            return services.ConfigureOptions<LibraryPaths>(config, sections);
        }

        public static IServiceCollection ConfigurePlayLocation(this IServiceCollection services, IConfiguration config, params string[] sections)
        {
            return services.ConfigureOptions<CurrentlyPlaying>(config, sections);
        }

        public static IServiceCollection ConfigureMusicLocation(this IServiceCollection services, IConfiguration config, params string[] sections)
        {
            return services.ConfigureOptions<MusicLibrary>(config, sections);
        }
    }
}
