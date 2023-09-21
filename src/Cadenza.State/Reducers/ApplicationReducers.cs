namespace Cadenza.State.Reducers;

public static class ApplicationReducers
{
    [ReducerMethod]
    public static ApplicationState ReduceApplicationStartedAction(ApplicationState state, ApplicationStartedAction action) 
    {
        return state with
        {
            StartedUp = true,
            Success = action.Success
        };
    }
}
