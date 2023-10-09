namespace Cadenza.Web.Actions.Reducers;

public static class GroupingsReducers
{
    [ReducerMethod]
    public static GroupingsState ReduceFetchGroupingsRequest(GroupingsState state, FetchGroupingsRequest action) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static GroupingsState ReduceFetchGroupingsResult(GroupingsState state, FetchGroupingsResult action) => state with
    {
        IsLoading = false,
        Groupings = action.Result
    };
}
