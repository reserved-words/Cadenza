namespace Cadenza.Web.Actions.Reducers;

public static class HistoryTopPlayedArtistsReducers
{
    [ReducerMethod]
    public static HistoryTopPlayedArtistsState ReduceTopPlayedArtistsRequest(HistoryTopPlayedArtistsState state, FetchTopPlayedArtistsRequest action) => state with
    {
        IsLoading = true,
        Period = action.Period,
        Items = new List<TopArtistVM>()
    };

    [ReducerMethod]
    public static HistoryTopPlayedArtistsState ReduceTopPlayedArtistsResult(HistoryTopPlayedArtistsState state, FetchTopPlayedArtistsResult action) => state with
    {
        IsLoading = false,
        Period = action.Period,
        Items = action.Result
    };
}
