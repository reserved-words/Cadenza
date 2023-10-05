namespace Cadenza.State.Actions.Reducers;

public static class RecentPlayHistoryReducers
{
    [ReducerMethod(typeof(FetchRecentPlayHistoryRequest))]
    public static RecentPlayHistoryState ReduceFetchRecentPlayHistoryAction(RecentPlayHistoryState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static RecentPlayHistoryState ReduceFetchRecentPlayHistoryResultAction(RecentPlayHistoryState state, FetchRecentPlayHistoryResult action) => state with
    {
        IsLoading = false,
        Tracks = action.Result
    };
}
