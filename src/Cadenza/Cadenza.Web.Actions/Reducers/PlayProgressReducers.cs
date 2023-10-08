namespace Cadenza.Web.Actions.Reducers;

public static class PlayProgressReducers
{
    [ReducerMethod(typeof(PlayProgressIncrementAction))]
    public static PlayProgressState ReducePlayProgressIncrementAction(PlayProgressState state)
    {
        var secondsPlayed = state.SecondsPlayed + 1;
        var secondsRemaining = state.SecondsRemaining - 1;
        var progress = 100 * (double)secondsPlayed / state.TotalSeconds;

        return state with
        {
            Progress = progress,
            SecondsPlayed = secondsPlayed,
            SecondsRemaining = secondsRemaining
        };
    }

    [ReducerMethod]
    public static PlayProgressState ReducePlayProgressResetAction(PlayProgressState state, PlayProgressResetAction action) => state with
    {
        Progress = 0,
        SecondsPlayed = 0,
        SecondsRemaining = action.TotalSeconds,
        TotalSeconds = action.TotalSeconds
    };

    [ReducerMethod]
    public static PlayProgressState ReducePlayProgressUpdateAction(PlayProgressState state, PlayProgressUpdateAction action)
    {
        var secondsPlayed = (int)Math.Floor(action.PercentagePlayed * state.TotalSeconds);

        return state with
        {
            Progress = action.PercentagePlayed,
            SecondsPlayed = secondsPlayed,
            SecondsRemaining = state.TotalSeconds - secondsPlayed
        };
    }
}
