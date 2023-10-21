namespace Cadenza.Web.Actions.Reducers;

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

    //[ReducerMethod]
    //public static EditableAlbumState ReduceTrackRemovedAction(EditableAlbumState state, TrackRemovedAction action)
    //{
    //    var track = state.Tracks.SingleOrDefault(t => t.TrackId == action.TrackId);

    //    if (track == null)
    //        return state;

    //    var updatedTracks = state.Tracks.ToList();
    //    updatedTracks.Remove(track);

    //    return state with
    //    {
    //        Tracks = new ReadOnlyCollection<AlbumTrackVM>(updatedTracks)
    //    };
    //}
}
