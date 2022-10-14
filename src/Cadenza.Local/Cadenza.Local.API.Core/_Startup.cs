global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Domain.Model.Updates;
global using Cadenza.Common.Interfaces.Utilities;
global using Cadenza.Local.API.Common.Controllers;
global using Cadenza.Local.API.Common.Interfaces;
global using Cadenza.Local.API.Core.Interfaces;
global using Cadenza.Local.API.Core.Services;
global using Cadenza.Local.API.Core.Settings;
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

        private static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IImageSrcGenerator, ImageSrcGenerator>()
                .AddTransient<IMusicDirectory, MusicDirectory>()
                .AddTransient<IWebImageService, WebImageService>();
        }
    }
}
