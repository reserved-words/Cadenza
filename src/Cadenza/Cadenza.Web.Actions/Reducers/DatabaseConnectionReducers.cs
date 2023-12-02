namespace Cadenza.Web.Actions.Reducers;

public static class DatabaseConnectionReducers
{
    [ReducerMethod(typeof(ApiConnectRequest))]
    public static ApiConnectionState ReduceDatabaseConnectRequest(ApiConnectionState state)
    {
        return state with
        {
            Title = "Connect to Database",
            State = ConnectionState.Connecting,
            Message = "Connecting"
        };
    }

    [ReducerMethod(typeof(ApiConnectionFailedAction))]
    public static ApiConnectionState ReduceDatabaseConnectionErroredAction(ApiConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Failed,
            Message = "Failed - see error log for details"
        };
    }

    [ReducerMethod(typeof(ApiConnectedAction))]
    public static ApiConnectionState ReduceDatabaseConnectedAction(ApiConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connected,
            Message = "Connected"
        };
    }
}
