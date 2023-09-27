using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Reducers;

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

    [ReducerMethod(typeof(DatabasePopulateRequest))]
    public static DatabaseConnectionState ReduceDatabasePopulateRequest(DatabaseConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connecting,
            Message = "Populating"
        };
    }

    [ReducerMethod(typeof(DatabaseConnectionErroredAction))]
    public static DatabaseConnectionState ReduceDatabaseConnectionErroredAction(DatabaseConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Failed,
            Message = "Errored"
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
