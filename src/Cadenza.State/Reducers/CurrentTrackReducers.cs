using Cadenza.State.Actions;
using Cadenza.State.Store;

namespace Cadenza.State.Reducers;

public static class CurrentTrackReducers
{
    [ReducerMethod(typeof(FetchCurrentTrackAction))]
    public static CurrentTrackState ReduceFetchCurrentTrackAction(CurrentTrackState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static CurrentTrackState ReduceFetchCurrentTrackResultAction(CurrentTrackState state, FetchCurrentTrackResultAction action) => state with
    {
        IsLoading = false,
        Track = action.Result
    };
}
