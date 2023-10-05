namespace Cadenza.State.Actions.Reducers;

public static class LastFmConnectionReducers
{
    [ReducerMethod(typeof(LastFmConnectRequest))]
    public static LastFmConnectionState ReduceLastFmConnectRequest(LastFmConnectionState state) 
    {
        return state with
        {
            Title = "Connect to Last.FM",
            State = ConnectionState.Connecting,
            Message = "Connecting"
        };
    }

    [ReducerMethod(typeof(LastFmFetchTokenRequest))]
    public static LastFmConnectionState ReduceLastFmFetchTokenRequest(LastFmConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connecting,
            Message = "Fetching auth token"
        };
    }

    [ReducerMethod(typeof(LastFmFetchTokenResult))]
    public static LastFmConnectionState ReduceLastFmFetchTokenResult(LastFmConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connecting,
            Message = "Saving auth token"
        };
    }

    [ReducerMethod(typeof(LastFmFetchSessionKeyRequest))]
    public static LastFmConnectionState ReduceLastFmFetchSessionKeyRequest(LastFmConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connecting,
            Message = "Fetching session key"
        };
    }

    [ReducerMethod]
    public static LastFmConnectionState ReduceLastFmFetchSessionKeyResult(LastFmConnectionState state, LastFmFetchSessionKeyResult action)
    {
        return state with
        {
            State = ConnectionState.Connecting,
            Message = "Saving session key",
            SessionKey = action.SessionKey
        };
    }

    [ReducerMethod(typeof(LastFmConnectionFailedAction))]
    public static LastFmConnectionState ReduceLastFmConnectionErroredAction(LastFmConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Failed,
            Message = "Failed - see error log for details"
        };
    }

    [ReducerMethod(typeof(LastFmConnectedAction))]
    public static LastFmConnectionState ReduceLastFmConnectedAction(LastFmConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connected,
            Message = "Connected"
        };
    }
}
