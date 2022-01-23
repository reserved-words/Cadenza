namespace Cadenza.Core;

public class TimingPlayer : IPlayer
{
    private readonly IPlayer _player;
    private readonly ITrackTimerController _timer;

    public TimingPlayer(IPlayer player, ITrackTimerController timer)
    {
        _player = player;
        _timer = timer;
    }

    public async Task<TrackProgress> Pause()
    {
        var progress = await _player.Pause();
        _timer.OnPause(progress.SecondsPlayed);
        return progress;
    }

    public async Task<TrackProgress> Play(BasicTrack track)
    {
        var progress = await _player.Play(track);
        _timer.OnSetTrack(progress.TotalSeconds);
        _timer.OnPlay();
        return progress;
    }

    public async Task<TrackProgress> Resume()
    {
        var progress = await _player.Resume();
        _timer.OnResume(progress.SecondsPlayed);
        return progress;
    }

    public async Task<TrackProgress> Stop()
    {
        var progress = await _player.Stop();
        _timer.OnStop(progress.SecondsPlayed);
        return progress;
    }
}