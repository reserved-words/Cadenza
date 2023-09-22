using System;

namespace Cadenza.State.Reducers;

public static class TrackRemovalReducers
{
    [ReducerMethod]
    public static TrackRemovalState ReduceTrackRemovalRequestAction(TrackRemovalState state, TrackRemovalRequest action) => state with
    {
        IsLoading = true,
        TrackId = action.TrackId,
        Succeeded = false
    };

    [ReducerMethod]
    public static TrackRemovalState ReduceTrackRemovedAction(TrackRemovalState state, TrackRemovedAction action) => state with
    {
        IsLoading = false,
        Succeeded = true
    };

    [ReducerMethod]
    public static TrackRemovalState ReduceTrackRemovalFailedAction(TrackRemovalState state, TrackRemovalFailedAction action) => state with
    {
        IsLoading = false,
        Succeeded = false
    };
}
