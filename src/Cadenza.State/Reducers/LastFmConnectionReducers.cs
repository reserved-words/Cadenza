using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Reducers;

public static class LastFmConnectionReducers
{
    [ReducerMethod(typeof(LastFmConnectRequest))]
    public static LastFmConnectionState ReduceLastFmConnectRequest(LastFmConnectionState state) 
    {
        return state with
        {
            State = TaskState.Running,
            Message = "Connecting"
        };
    }

    [ReducerMethod(typeof(LastFmFetchTokenRequest))]
    public static LastFmConnectionState ReduceLastFmFetchTokenRequest(LastFmConnectionState state)
    {
        return state with
        {
            State = TaskState.Running,
            Message = "Fetching auth token"
        };
    }

    [ReducerMethod(typeof(LastFmFetchTokenResult))]
    public static LastFmConnectionState ReduceLastFmFetchTokenResult(LastFmConnectionState state)
    {
        return state with
        {
            State = TaskState.Running,
            Message = "Saving auth token"
        };
    }

    [ReducerMethod(typeof(LastFmFetchSessionKeyRequest))]
    public static LastFmConnectionState ReduceLastFmFetchSessionKeyRequest(LastFmConnectionState state)
    {
        return state with
        {
            State = TaskState.Running,
            Message = "Fetching session key"
        };
    }

    [ReducerMethod]
    public static LastFmConnectionState ReduceLastFmFetchSessionKeyResult(LastFmConnectionState state, LastFmFetchSessionKeyResult action)
    {
        return state with
        {
            State = TaskState.Running,
            Message = "Saving session key",
            SessionKey = action.SessionKey
        };
    }

    [ReducerMethod(typeof(LastFmConnectionErroredAction))]
    public static LastFmConnectionState ReduceLastFmConnectionErroredAction(LastFmConnectionState state)
    {
        return state with
        {
            State = TaskState.Errored,
            Message = "Errored"
        };
    }

    [ReducerMethod(typeof(LastFmConnectedAction))]
    public static LastFmConnectionState ReduceLastFmConnectedAction(LastFmConnectionState state)
    {
        return state with
        {
            State = TaskState.Completed,
            Message = "Connected"
        };
    }
}
