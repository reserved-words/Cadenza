using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Core.Utilities;

namespace Cadenza.Web.Core;

public static class Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddUtilities();

        services
            .AddTransient<IAppStore, Store>()
            .AddTransient<IImageFinder, ImageFinder>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IUrl, Url>();

        return services;
    }
}
