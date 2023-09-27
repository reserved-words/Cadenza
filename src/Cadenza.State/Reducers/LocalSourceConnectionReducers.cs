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

    [ReducerMethod(typeof(LocalSourceConnectionErroredAction))]
    public static LocalSourceConnectionState ReduceLocalSourceConnectionErroredAction(LocalSourceConnectionState state)
    {
        return state with
        {
            State = ConnectionState.Failed,
            Message = "Errored"
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
