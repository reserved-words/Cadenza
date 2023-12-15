namespace Cadenza.Web.Actions.Reducers;

public static class HistoryRecentlyPlayedTracksReducers
{
    [ReducerMethod(typeof(FetchRecentlyPlayedTracksRequest))]
    public static HistoryRecentlyPlayedTracksState ReduceRecentlyPlayedTracksRequest(HistoryRecentlyPlayedTracksState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static HistoryRecentlyPlayedTracksState ReduceRecentlyPlayedTracksResult(HistoryRecentlyPlayedTracksState state, FetchRecentlyPlayedTracksResult action) => state with
    {
        IsLoading = false,
        Tracks = action.Result
    };

    
}
