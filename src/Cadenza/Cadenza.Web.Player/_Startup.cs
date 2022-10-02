global using Cadenza.Web.Common.Interfaces;
global using Microsoft.Extensions.DependencyInjection;
global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Web.Common.Events;
global using Microsoft.AspNetCore.Components;
global using Cadenza.Web.Common.Enums;
using Cadenza.Web.Player.Interfaces;
using Cadenza.Web.Player.Players;

namespace Cadenza.Web.Player;

public static class _Startup
{
    public static IServiceCollection AddPlayerComponent(this IServiceCollection services)
    {
        return services
            .AddTransient<IUtilityPlayer, TimingPlayer>()
            .AddSingleton<TrackTimer>()
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<IPlayer, CorePlayer>()
            .AddTransient<IUtilityPlayer, TrackingPlayer>();
    }
}
