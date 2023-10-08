namespace Cadenza.Web.Actions.Reducers;

public static class ViewTrackReducers
{
    [ReducerMethod(typeof(FetchViewTrackRequest))]
    public static ViewTrackState ReduceFetchViewTrackAction(ViewTrackState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewTrackState ReduceFetchViewTrackResult(ViewTrackState state, FetchViewTrackResult action) => state with
    {
        IsLoading = false,
        Track = action.Track
    };

    [ReducerMethod]
    public static ViewTrackState ReduceTrackUpdatedAction(ViewTrackState state, TrackUpdatedAction action)
    {
        if (state.Track == null || state.Track.Id != action.UpdatedTrack.Id)
            return state;

        var track = state.Track with { Track = action.UpdatedTrack };

        return state with { Track = track };
    }
}
