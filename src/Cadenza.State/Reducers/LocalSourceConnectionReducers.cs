using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Reducers;

public static class LocalSourceConnectionReducers
{
    [ReducerMethod(typeof(LocalSourceConnectRequest))]
    public static LocalSourceConnectionState ReduceLocalSourceConnectRequest(LocalSourceConnectionState state) 
    {
        return state with
        {
            State = TaskState.Running,
            Message = "Populating"
        };
    }

    [ReducerMethod(typeof(LocalSourceConnectionErroredAction))]
    public static LocalSourceConnectionState ReduceLocalSourceConnectionErroredAction(LocalSourceConnectionState state)
    {
        return state with
        {
            State = TaskState.Errored,
            Message = "Errored"
        };
    }

    [ReducerMethod(typeof(LocalSourceConnectedAction))]
    public static LocalSourceConnectionState ReduceLocalSourceConnectedAction(LocalSourceConnectionState state)
    {
        return state with
        {
            State = TaskState.Completed,
            Message = "Connected"
        };
    }
}
