global using Cadenza.Common.Enums;
global using Cadenza.Common.Enums.Extensions;
global using Cadenza.Common.Http.Interfaces;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Core.Services;
global using Cadenza.Web.State.Actions;
global using Cadenza.Web.State.Store;
global using Fluxor;
global using Microsoft.Extensions.DependencyInjection;
global using System.Web;
global using Cadenza.Web.Api.Interfaces;

using Cadenza.Common.Utilities;
using Cadenza.Web.Core.Player;

namespace Cadenza.Web.Core;

public static class Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddUtilities();

        services
            .AddTransient<IImageFinder, ImageFinder>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>();

        return services;
    }

    public static IServiceCollection AddPlayer(this IServiceCollection services)
    {
        return services
            .AddScoped<IPlayTimer, PlayTimer>()
            .AddScoped<IPlayer, CorePlayer>();
    }
}
