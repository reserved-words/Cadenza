using Cadenza.State.Actions;
using Cadenza.State.Store;

namespace Cadenza.State.Reducers;

public static class RecentPlayHistoryReducers
{
    [ReducerMethod(typeof(FetchRecentPlayHistoryAction))]
    public static RecentPlayHistoryState ReduceFetchRecentPlayHistoryAction(RecentPlayHistoryState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static RecentPlayHistoryState ReduceFetchRecentPlayHistoryResultAction(RecentPlayHistoryState state, FetchRecentPlayHistoryResultAction action) => state with
    {
        IsLoading = false,
        Tracks = action.Result
    };
}
