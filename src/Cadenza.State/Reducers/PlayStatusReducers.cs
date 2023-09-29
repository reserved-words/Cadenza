namespace Cadenza.State.Reducers;

public static class PlayStatusReducers
{
    [ReducerMethod(typeof(PlayStatusPausedAction))]
    public static PlayStatusState ReducePlayStatusPauseAction(PlayStatusState state) => state with
    {
        Status = PlayStatus.Paused
    };

    [ReducerMethod(typeof(PlayStatusResumedAction))]
    public static PlayStatusState ReducePlayStatusResumedAction(PlayStatusState state) => state with
    {
        Status = PlayStatus.Playing
    };

    [ReducerMethod(typeof(PlayStatusStoppedAction))]
    public static PlayStatusState ReducePlayStatusStopAction(PlayStatusState state) => state with
    {
        Status = PlayStatus.Stopped
    };

    [ReducerMethod]
    public static PlayStatusState ReducePlayStatusStopAction(PlayStatusState state, PlayStatusPlayingAction action)
    {
        return state with
        {
            Status = PlayStatus.Playing
        };
    }
}
