namespace Cadenza.Web.Actions.Reducers;

public static class HistoryRecentlyPlayedAlbumsReducers
{
    [ReducerMethod(typeof(FetchRecentlyPlayedAlbumsRequest))]
    public static HistoryRecentlyPlayedAlbumsState ReduceRecentlyPlayedAlbumsRequest(HistoryRecentlyPlayedAlbumsState state) => state with
    {
        IsLoading = true,
        Items = new List<RecentAlbumVM>()
    };

    [ReducerMethod]
    public static HistoryRecentlyPlayedAlbumsState ReduceRecentlyPlayedAlbumsResult(HistoryRecentlyPlayedAlbumsState state, FetchRecentlyPlayedAlbumsResult action) => state with
    {
        IsLoading = false,
        Items = action.Result
    };
}
