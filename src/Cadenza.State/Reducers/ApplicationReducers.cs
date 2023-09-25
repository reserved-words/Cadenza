namespace Cadenza.State.Reducers;

public static class ApplicationReducers
{
    [ReducerMethod(typeof(ApplicationStartedAction))]
    public static ApplicationState ReduceApplicationStartedAction(ApplicationState state) 
    {
        return state with
        {
            Started = true
        };
    }
}
