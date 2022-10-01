global using Cadenza.Web.Common.Interfaces;
global using Microsoft.Extensions.DependencyInjection;
global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Interfaces.Repositories;
global using Cadenza.Web.Common.Events;
global using Microsoft.AspNetCore.Components;

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
