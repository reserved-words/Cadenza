using Cadenza.Core.Model;

namespace Cadenza.Core.Player;

public class TimingPlayer : IUtilityPlayer
{
    private readonly ITrackTimerController _timer;

    public TimingPlayer(ITrackTimerController timer)
    {
        _timer = timer;
    }

    public Task OnPause(TrackProgress progress)
    {
        _timer.OnPause(progress.SecondsPlayed);
        return Task.CompletedTask;
    }

    public Task OnPlay(TrackProgress progress)
    {
        _timer.OnPlay(progress.TotalSeconds);
        return Task.CompletedTask;
    }

    public Task OnResume(TrackProgress progress)
    {
        _timer.OnResume(progress.SecondsPlayed);
        return Task.CompletedTask;
    }

    public Task OnStop(TrackProgress progress)
    {
        _timer.OnStop(progress.SecondsPlayed);
        return Task.CompletedTask;
    }
}