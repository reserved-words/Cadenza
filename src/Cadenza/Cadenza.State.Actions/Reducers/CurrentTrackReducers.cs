namespace Cadenza.State.Actions.Reducers;

public static class CurrentTrackReducers
{
    [ReducerMethod(typeof(FetchTrackRequest))]
    public static CurrentTrackState ReduceFetchCurrentTrackAction(CurrentTrackState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static CurrentTrackState ReduceFetchCurrentTrackResultAction(CurrentTrackState state, UpdateCurrentTrackAction action) => state with
    {
        IsLoading = false,
        Track = action.FullTrack,
        IsLastInPlaylist = action.IsLastTrackInPlaylist
    };
}
