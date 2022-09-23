using Cadenza.Local.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Local
{
    public static class Startup
    {
        public static IServiceCollection ConfigurePlayLocation(this IServiceCollection services, IConfiguration config, string sectionPath)
        {
            var section = config.GetSection(sectionPath);
            return services.Configure<CurrentlyPlaying>(section);
        }

        public static IServiceCollection ConfigureMusicLocation(this IServiceCollection services, IConfiguration config, string sectionPath)
        {
            var section = config.GetSection(sectionPath);
            return services.Configure<MusicLibrary>(section);
        }
    }
}
