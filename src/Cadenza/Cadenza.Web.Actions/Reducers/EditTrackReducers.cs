namespace Cadenza.Web.Actions.Reducers;

public static class EditTrackReducers
{
    [ReducerMethod(typeof(FetchEditTrackRequest))]
    public static EditTrackState ReduceFetchEditTrackAction(EditTrackState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static EditTrackState ReduceFetchEditTrackResult(EditTrackState state, FetchEditTrackResult action) => state with
    {
        IsLoading = false,
        Track = action.Track
    };

    [ReducerMethod]
    public static EditTrackState ReduceViewEditEndRequest(EditTrackState state, ViewEditEndRequest action) => state with
    {
        IsLoading = false,
        Track = null
    };
}
