global using Cadenza.Domain.Model;
global using Cadenza.Domain.Model.Track;
global using Cadenza.Domain.Model.Updates;

global using Cadenza.Local.API.Common.Interfaces;
global using Cadenza.Local.API.Common.Controllers;
global using Cadenza.Local.API.Core.Interfaces;
global using Cadenza.Local.API.Core.Services;
global using Cadenza.Local.API.Core.Settings;

global using Cadenza.Utilities.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Local.API.Core
{
    public static class Startup
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services.AddInternalServices()
                .AddTransient<ILibraryService, LibraryService>()
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
