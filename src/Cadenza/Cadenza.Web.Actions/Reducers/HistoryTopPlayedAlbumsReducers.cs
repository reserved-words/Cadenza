namespace Cadenza.Web.Actions.Reducers;

public static class HistoryTopPlayedAlbumsReducers
{
    [ReducerMethod]
    public static HistoryTopPlayedAlbumsState ReduceTopPlayedAlbumsRequest(HistoryTopPlayedAlbumsState state, FetchTopPlayedAlbumsRequest action) => state with
    {
        IsLoading = true,
        Period = action.Period,
        Items = new List<TopAlbumVM>()
    };

    [ReducerMethod]
    public static HistoryTopPlayedAlbumsState ReduceTopPlayedAlbumsResult(HistoryTopPlayedAlbumsState state, FetchTopPlayedAlbumsResult action) => state with
    {
        IsLoading = false,
        Period = action.Period,
        Items = action.Result
    };
}
