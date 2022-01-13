namespace Cadenza.Player;

public class TimingPlayer : IPlayer
{
    private readonly IPlayer _player;
    private readonly ITrackTimerController _timer;

    public TimingPlayer(IPlayer player, ITrackTimerController timer)
    {
        _player = player;
        _timer = timer;
    }

    public async Task<int> Pause()
    {
        var secondsPlayed = await _player.Pause();
        _timer.OnPause(secondsPlayed);
        return secondsPlayed;
    }

    public async Task Play(PlayingTrack track)
    {
        await _player.Play(track);
        _timer.OnSetTrack(track?.DurationSeconds ?? 0);
        _timer.OnPlay();
    }

    public async Task<int> Resume()
    {
        var secondsPlayed = await _player.Resume();
        _timer.OnResume(secondsPlayed);
        return secondsPlayed;
    }

    public async Task<int> Stop()
    {
        var secondsPlayed = await _player.Stop();
        _timer.OnStop(secondsPlayed);
        return secondsPlayed;
    }
}