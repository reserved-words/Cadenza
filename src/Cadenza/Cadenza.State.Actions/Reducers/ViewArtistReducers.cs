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

    [ReducerMethod]
    public static ViewArtistState ReduceArtistUpdatedAction(ViewArtistState state, ArtistUpdatedAction action)
    {
        if (state.Artist == null || state.Artist.Id == action.UpdatedArtist.Id)
            return state;

        return state with { Artist = action.UpdatedArtist };
    }
}
