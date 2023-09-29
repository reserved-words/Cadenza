namespace Cadenza.State.Reducers;

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
}
