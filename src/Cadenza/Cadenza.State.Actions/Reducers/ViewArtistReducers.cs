namespace Cadenza.State.Actions.Reducers;

public static class ViewArtistReducers
{
    [ReducerMethod(typeof(FetchViewArtistRequest))]
    public static ViewArtistState ReduceFetchViewArtistAction(ViewArtistState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewArtistState ReduceFetchViewArtistResult(ViewArtistState state, FetchViewArtistResult action) => state with
    {
        IsLoading = false,
        Artist = action.Artist,
        Releases = action.Releases
    };
}
