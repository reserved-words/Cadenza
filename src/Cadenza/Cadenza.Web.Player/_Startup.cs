﻿global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.State.Actions;
global using Fluxor;

using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Web.Player;

public static class Startup
{
    public static IServiceCollection AddPlayer(this IServiceCollection services)
    {
        return services
            .AddScoped<IPlayTimer, PlayTimer>()
            .AddScoped<IPlayer, CorePlayer>();
    }
}
