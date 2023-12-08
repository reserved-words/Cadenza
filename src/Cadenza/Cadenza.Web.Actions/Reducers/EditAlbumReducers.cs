namespace Cadenza.Web.Actions.Reducers;

public static class EditAlbumReducers
{
    [ReducerMethod(typeof(FetchEditAlbumRequest))]
    public static EditAlbumState ReduceFetchEditAlbumAction(EditAlbumState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static EditAlbumState ReduceFetchEditAlbumResult(EditAlbumState state, FetchEditAlbumResult action) => state with
    {
        IsLoading = false,
        Album = action.Album,
        Tracks = action.Tracks
    };

    [ReducerMethod]
    public static EditAlbumState ReduceViewEditEndRequest(EditAlbumState state, ViewEditEndRequest action) => state with
    {
        IsLoading = false,
        Album = null,
        Tracks = null
    };
}
