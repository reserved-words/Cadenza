namespace Cadenza.State.Actions.Reducers;

public static class ViewAlbumReducers
{
    [ReducerMethod(typeof(FetchViewAlbumRequest))]
    public static ViewAlbumState ReduceFetchViewAlbumAction(ViewAlbumState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewAlbumState ReduceFetchViewAlbumResult(ViewAlbumState state, FetchViewAlbumResult action) => state with
    {
        IsLoading = false,
        Album = action.Album,
        Discs = action.Discs
    };
}
