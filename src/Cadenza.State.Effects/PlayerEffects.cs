using Cadenza.State.Actions;
using Cadenza.Web.Common.Interfaces;
using Fluxor;
using System;

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
        await _player.Play(action.Track.Track);
        dispatcher.Dispatch(new PlayStatusPlayingAction());
    }


    [EffectMethod]
    public async Task HandlePlayerPauseAction(PlayerPauseRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Pause();
        dispatcher.Dispatch(new PlayStatusPausedAction(secondsPlayed));
    }


    [EffectMethod]
    public async Task HandlePlayerResumeAction(PlayerResumeRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Resume();
        dispatcher.Dispatch(new PlayStatusResumedAction(secondsPlayed));
    }


    [EffectMethod]
    public async Task HandlePlayerStopAction(PlayerStopRequest action, IDispatcher dispatcher)
    {
        var secondsPlayed = await _player.Stop();
        dispatcher.Dispatch(new PlayStatusStoppedAction(secondsPlayed));
    }
}
