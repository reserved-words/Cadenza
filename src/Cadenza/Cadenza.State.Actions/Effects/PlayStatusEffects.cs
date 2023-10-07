namespace Cadenza.State.Actions.Effects;

public class PlayStatusEffects
{
    [EffectMethod]
    public Task HandlePlayStatusPlayingAction(PlayStatusPlayingAction action, IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Playing, action.Track, 0);
    }

    [EffectMethod]
    public Task HandlePlayStatusPausedAction(PlayStatusPausedAction action, IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Paused, action.Track, action.SecondsPlayed);
    }

    [EffectMethod]
    public Task HandlePlayStatusResumedAction(PlayStatusResumedAction action, IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Playing, action.Track, action.SecondsPlayed);
    }

    [EffectMethod]
    public Task HandlePlayStatusStoppedAction(PlayStatusStoppedAction action, IDispatcher dispatcher)
    {
        return RequestUpdateRecentPlayHistory(dispatcher, PlayStatus.Stopped, action.Track, action.SecondsPlayed);
    }

    private Task RequestUpdateRecentPlayHistory(IDispatcher dispatcher, PlayStatus playStatus, TrackFullVM track, int secondsPlayed)
    {
        dispatcher.Dispatch(new UpdateRecentPlayHistoryRequest(playStatus, track, secondsPlayed));
        return Task.CompletedTask;
    }
}
