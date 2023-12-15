namespace Cadenza.Web.Actions.Reducers;

public static class HistoryRecentlyAddedAlbumsReducers
{
    [ReducerMethod(typeof(FetchRecentlyAddedAlbumsRequest))]
    public static HistoryRecentlyAddedAlbumsState ReduceRecentlyAddedAlbumsRequest(HistoryRecentlyAddedAlbumsState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static HistoryRecentlyAddedAlbumsState ReduceRecentlyAddedAlbumsResult(HistoryRecentlyAddedAlbumsState state, FetchRecentlyAddedAlbumsResult action) => state with
    {
        IsLoading = false,
        Albums = action.Result
    };
}
