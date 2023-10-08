using Cadenza.Web.State.Actions;

namespace Cadenza.Web.Actions.Effects;

public class PlayerEffects
{
    private readonly IPlayer _player;
    private readonly IPlayTimer _playTimer;

    public PlayerEffects(IPlayer player, IPlayTimer playTimer)
    {
        _player = player;
        _playTimer = playTimer;
    }

    [EffectMethod]
    public async Task HandlePlayerPlayRequest(PlayerPlayRequest action, IDispatcher dispatcher)
    {
        await _player.Play();
        _playTimer.OnPlay(action.Track.Duration);
        dispatcher.Dispatch(new PlayStatusPlayingAction(action.Track));
    }


    [EffectMethod]
    public async Task HandlePlayerPauseRequest(PlayerPauseRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Pause();
        _playTimer.OnPause();
        dispatcher.Dispatch(new PlayStatusPausedAction(action.Track, secondsPlayed));
    }


    [EffectMethod]
    public async Task HandlePlayerResumeRequest(PlayerResumeRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Resume();
        _playTimer.OnResume();
        dispatcher.Dispatch(new PlayStatusResumedAction(action.Track, secondsPlayed));
    }


    [EffectMethod]
    public async Task HandlePlayerStopRequest(PlayerStopRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Stop();
        _playTimer.OnStop();
        dispatcher.Dispatch(new PlayStatusStoppedAction(action.Track, secondsPlayed));
    }
}
