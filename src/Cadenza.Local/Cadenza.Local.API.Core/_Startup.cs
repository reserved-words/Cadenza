using Cadenza.Local.API.Common.Controllers;
using Cadenza.Local.API.Core.Interfaces;
using Cadenza.Local.API.Core.Services;
using Cadenza.Local.API.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Local.API.Core
{
    public static class Startup
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services.AddInternalServices()
                .AddTransient<IArtworkService, ArtworkService>()
                .AddTransient<IPlayService, PlayService>()
                .AddTransient<ISyncService, SyncService>();
        }

        public static IServiceCollection ConfigureMusicLocation(this IServiceCollection services, IConfiguration config, string sectionPath)
        {
            var section = config.GetSection(sectionPath);
            return services.Configure<MusicLibrarySettings>(section);
        }
        private static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IImageSrcGenerator, ImageSrcGenerator>()
                .AddTransient<IMusicDirectory, MusicDirectory>();
        }
    }
}
