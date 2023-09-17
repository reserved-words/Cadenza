using Cadenza.State.Actions;
using Cadenza.State.Store;
using Cadenza.Web.Common.Enums;
using Fluxor;
using System;

namespace Cadenza.State.Effects;

public class PlayStatusEffects
{
    private readonly IState<CurrentTrackState> _currentTrackState;

    public PlayStatusEffects(IState<CurrentTrackState> currentTrackState)
    {
        _currentTrackState = currentTrackState;
    }

    [EffectMethod(typeof(PlayStatusPlayingAction))]
    public Task HandlePlayStatusPlayingAction(IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Playing, 0);
    }

    [EffectMethod]
    public Task HandlePlayStatusPausedAction(PlayStatusPausedAction action, IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Paused, action.SecondsPlayed);
    }

    [EffectMethod]
    public Task HandlePlayStatusResumedAction(PlayStatusResumedAction action, IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Playing, action.SecondsPlayed);
    }

    [EffectMethod]
    public Task HandlePlayStatusStoppedAction(PlayStatusStoppedAction action, IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Stopped, action.SecondsPlayed);
    }

    private Task RequestUpdateRecentPlayHistory(IDispatcher dispatcher, PlayStatus playStatus, int secondsPlayed)
    {
        dispatcher.Dispatch(new UpdateRecentPlayHistoryRequest(playStatus, _currentTrackState.Value.FullTrack, secondsPlayed));
        return Task.CompletedTask;
    }
}
