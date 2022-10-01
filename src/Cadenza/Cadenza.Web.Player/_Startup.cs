using Cadenza.Web.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Web.Player;

public static class _Startup
{
    public static IServiceCollection AddPlayerComponent(this IServiceCollection services)
    {
        return services
            .AddTransient<IUtilityPlayer, TimingPlayer>()
            .AddSingleton<TrackTimer>()
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackProgressedConsumer>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackFinishedConsumer>(sp => sp.GetRequiredService<TrackTimer>());
    }
}
