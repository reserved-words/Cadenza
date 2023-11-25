namespace Cadenza.Web.Actions.Reducers;

public static class DatabaseConnectionReducers
{
    [ReducerMethod(typeof(DatabaseConnectRequest))]
    public static DatabaseConnectionState ReduceDatabaseConnectRequest(DatabaseConnectionState state)
    {
        return state with
        {
            Title = "Connect to Database",
            State = ConnectionState.Connecting,
            Message = "Connecting"
        };
    }

    [ReducerMethod(typeof(DatabaseConnectionFailedAction))]
    public static DatabaseConnectionState ReduceDatabaseConnectionErroredAction(DatabaseConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Failed,
            Message = "Failed - see error log for details"
        };
    }

    [ReducerMethod(typeof(DatabaseConnectedAction))]
    public static DatabaseConnectionState ReduceDatabaseConnectedAction(DatabaseConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connected,
            Message = "Connected"
        };
    }
}
