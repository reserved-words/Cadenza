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
    public async Task HandlePlayerPlayAction(PlayerPlayRequest action, IDispatcher dispatcher)
    {
        await _player.Play(action.Track);
        dispatcher.Dispatch(new PlayStatusPlayingAction());
    }


    [EffectMethod(typeof(PlayerPauseRequest))]
    public async Task HandlePlayerPauseAction(IDispatcher dispatcher)
    {
        await _player.Pause();
        dispatcher.Dispatch(new PlayStatusPausedAction());
    }


    [EffectMethod(typeof(PlayerResumeRequest))]
    public async Task HandlePlayerResumeAction(IDispatcher dispatcher)
    {
        await _player.Resume();
        dispatcher.Dispatch(new PlayStatusResumedAction());
    }


    [EffectMethod(typeof(PlayerStopRequest))]
    public async Task HandlePlayerStopAction(IDispatcher dispatcher)
    {
        await _player.Stop();
        dispatcher.Dispatch(new PlayStatusStoppedAction());
    }
}
