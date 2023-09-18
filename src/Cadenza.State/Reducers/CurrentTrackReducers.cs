﻿using Cadenza.State.Actions;
using Cadenza.State.Store;

namespace Cadenza.State.Reducers;

public static class CurrentTrackReducers
{
    [ReducerMethod(typeof(FetchFullTrackRequest))]
    public static CurrentTrackState ReduceFetchCurrentTrackAction(CurrentTrackState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static CurrentTrackState ReduceFetchCurrentTrackResultAction(CurrentTrackState state, UpdateCurrentTrackAction action) => state with
    {
        IsLoading = false,
        FullTrack = action.FullTrack,
        IsLastInPlaylist = action.IsLastTrackInPlaylist
    };
}
