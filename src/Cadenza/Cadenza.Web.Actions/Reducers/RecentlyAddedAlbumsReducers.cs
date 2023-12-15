namespace Cadenza.Web.Actions.Reducers;

public static class RecentlyAddedAlbumsReducers
{
    [ReducerMethod(typeof(FetchRecentlyAddedAlbumsRequest))]
    public static RecentlyAddedAlbumsState ReduceFetchRecentlyAddedAlbumsRequest(RecentlyAddedAlbumsState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static RecentlyAddedAlbumsState ReduceFetchRecentlyAddedAlbumsResult(RecentlyAddedAlbumsState state, FetchRecentlyAddedAlbumsResult action) => state with
    {
        IsLoading = false,
        Albums = action.Result
    };
}
