global using Microsoft.AspNetCore.Components;
global using Microsoft.Extensions.DependencyInjection;

global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model.Track;

global using Cadenza.Web.Common.Events;
global using Cadenza.Web.Common.Interfaces;

global using Cadenza.Web.Player.Players;

namespace Cadenza.Web.Player;

public static class _Startup
{
    public static IServiceCollection AddPlayerComponent(this IServiceCollection services)
    {
        return services
            .AddScoped<IPlayTimer, PlayTimer>()
            .AddScoped<IPlayer, CorePlayer>();
    }
}
