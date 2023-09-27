using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Reducers;

public static class LocalSourceConnectionReducers
{
    [ReducerMethod(typeof(LocalSourceConnectRequest))]
    public static LocalSourceConnectionState ReduceLocalSourceConnectRequest(LocalSourceConnectionState state) 
    {
        return state with
        {
            Title = "Connect to Local Library",
            State = ConnectionState.Connecting,
            Message = "Connecting"
        };
    }

    [ReducerMethod(typeof(LocalSourceConnectionFailedAction))]
    public static LocalSourceConnectionState ReduceLocalSourceConnectionFailedAction(LocalSourceConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Failed,
            Message = "Failed - see error log for details"
        };
    }

    [ReducerMethod(typeof(LocalSourceConnectedAction))]
    public static LocalSourceConnectionState ReduceLocalSourceConnectedAction(LocalSourceConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Connected,
            Message = "Connected"
        };
    }
}
