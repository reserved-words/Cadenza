global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Extensions;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Interfaces.Library;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Core.Services;
global using Microsoft.Extensions.DependencyInjection;
global using System.Web;
global using Cadenza.Web.Common.ViewModels;

using Cadenza.Common.Utilities;
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
