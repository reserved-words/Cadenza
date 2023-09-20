using Cadenza.State.Actions;
using Cadenza.State.Store;

namespace Cadenza.State.Reducers;

public static class TrackRemovalReducers
{
    [ReducerMethod(typeof(TrackRemovalRequest))]
    public static TrackRemovalState ReduceFetchCurrentTrackAction(TrackRemovalState state) => state with
    {
        IsLoading = true,
        LastTrackRemovedId = 0,
        Error = null
    };

    [ReducerMethod]
    public static TrackRemovalState ReduceFetchCurrentTrackResultAction(TrackRemovalState state, TrackRemovedAction action) => state with
    {
        IsLoading = false,
        LastTrackRemovedId = action.TrackId,
        Error = action.Error
    };
}
