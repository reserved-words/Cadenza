using Cadenza.Web.Core.Utilities;

namespace Cadenza.Web.Core;

public static class Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddUtilities();

        services
            .AddTransient<IImageFinder, ImageFinder>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IUrl, Url>();

        return services;
    }
}
