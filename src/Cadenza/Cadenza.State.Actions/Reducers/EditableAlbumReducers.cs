namespace Cadenza.State.Actions.Reducers;

public static class EditableAlbumReducers
{
    [ReducerMethod]
    public static EditableAlbumState ReduceFetchEditableAlbumTracksRequest(EditableAlbumState state, FetchEditableAlbumTracksRequest action) 
    {
        return state with
        {
            IsLoading = true,
            Tracks = new List<AlbumTrackVM>()
        };
    }

    [ReducerMethod]
    public static EditableAlbumState ReduceFetchEditableAlbumTracksResultAction(EditableAlbumState state, FetchEditableAlbumTracksResultAction action)
    {
        return state with
        {
            IsLoading = false,
            Tracks = action.Tracks
        };
    }

    [ReducerMethod]
    public static EditableAlbumState ReduceResetEditableAlbumTracksRequest(EditableAlbumState state, ResetEditableAlbumTracksRequest action)
    {
        return state with
        {
            IsLoading = false,
            Tracks = new List<AlbumTrackVM>()
        };
    }
}
