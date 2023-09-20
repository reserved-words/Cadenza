using Cadenza.State.Actions;
using Cadenza.Web.Common.Interfaces;
using Fluxor;

namespace Cadenza.State.Effects;

public class PlayerEffects
{
    private readonly IPlayer _player;

    public PlayerEffects(IPlayer player)
    {
        _player = player;
    }

    [EffectMethod]
    public async Task HandlePlayerPlayRequest(PlayerPlayRequest action, IDispatcher dispatcher)
    {
        await _player.Play(action.Track.Track);
        dispatcher.Dispatch(new PlayStatusPlayingAction(action.Track));
    }


    [EffectMethod]
    public async Task HandlePlayerPauseRequest(PlayerPauseRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Pause(action.Track.Track);
        dispatcher.Dispatch(new PlayStatusPausedAction(action.Track, secondsPlayed));
    }


    [EffectMethod]
    public async Task HandlePlayerResumeRequest(PlayerResumeRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Resume(action.Track.Track);
        dispatcher.Dispatch(new PlayStatusResumedAction(action.Track, secondsPlayed));
    }


    [EffectMethod]
    public async Task HandlePlayerStopRequest(PlayerStopRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Stop(action.Track.Track);
        dispatcher.Dispatch(new PlayStatusStoppedAction(action.Track, secondsPlayed));
    }
}
