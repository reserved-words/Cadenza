namespace Cadenza.Web.Actions.Reducers;

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
        Tracks = action.Tracks
    };

    [ReducerMethod]
    public static ViewAlbumState ReduceAlbumTracksUpdatedAction(ViewAlbumState state, AlbumTracksUpdatedAction action)
    {
        if (state.Album.Id != action.AlbumId)
            return state;

        return state with
        {
            Tracks = action.UpdatedTracks
        };
    }

    [ReducerMethod]
    public static ViewAlbumState ReduceAlbumUpdatedAction(ViewAlbumState state, AlbumUpdatedAction action)
    {
        if (state.Album == null || state.Album.Id != action.UpdatedAlbum.Id)
            return state;

        return state with { Album = action.UpdatedAlbum };
    }
}
