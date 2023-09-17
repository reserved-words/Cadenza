using Cadenza.State.Actions;
using Cadenza.State.Store;

namespace Cadenza.State.Reducers;

public static class PlaylistTrackReducers
{
    [ReducerMethod]
    public static PlaylistTrackState ReducePlaylistTrackUpdateAction(PlaylistTrackState state, PlaylistTrackUpdateAction action) => state with
    {
        IsLoading = action.IsLoading,
        CurrentTrack = action.Track,
        IsCurrentTrackLast = action.IsLastTrack
    };
}
