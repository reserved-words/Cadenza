namespace Cadenza.State.Reducers;

public static class PlaylistQueueReducers
{
    [ReducerMethod]
    public static PlaylistQueueState ReducePlaylistQueueUpdateRequest(PlaylistQueueState state, PlaylistQueueUpdateRequest action)
    {
        // Note this doesn't take into consideration starting on a specific track - find where that comes in in original code

        if (action.Definition == null)
        {
            return PlaylistQueueState.Init();
        }

        var allTracks = action.Definition.Tracks.ToList();
        var toPlay = PopulateToPlay(allTracks);
        var played = PopulatePlayed();
        var playing = toPlay.Pop();

        return state with
        {
            IsLoading = false,
            Played = played,
            ToPlay = toPlay,
            CurrentTrack = playing
        };
    }

    [ReducerMethod]
    public static PlaylistQueueState ReducePlaylistQueueMoveNextRequest(PlaylistQueueState state, PlaylistQueueMoveNextRequest action)
    {
        state.Played.Push(state.CurrentTrack);

        var playing = state.ToPlay.Count == 0
            ? 0
            : state.ToPlay.Pop();

        return state with
        {
            IsLoading = false,
            CurrentTrack = playing
        };
    }

    [ReducerMethod]
    public static PlaylistQueueState ReducePlaylistQueueMovePreviousRequest(PlaylistQueueState state, PlaylistQueueMovePreviousRequest action)
    {
        var playing = state.CurrentTrack;

        if (state.Played.Count > 0)
        {
            if (playing != 0)
            {
                state.ToPlay.Push(playing);
            }
            
            playing = state.Played.Pop();
        }

        return state with
        {
            IsLoading = false,
            CurrentTrack = playing
        };
    }

    [ReducerMethod]
    public static PlaylistQueueState ReducePlaylistQueueRemoveTrackRequest(PlaylistQueueState state, PlaylistQueueRemoveTrackRequest action)
    {
        var playedTracks = new List<int>(state.Played);
        playedTracks.RemoveAll(t => t == action.TrackId);
        PopulatePlayed(playedTracks);

        var toPlayTracks = new List<int>(state.ToPlay);
        toPlayTracks.RemoveAll(t => t == action.TrackId);
        PopulateToPlay(toPlayTracks);

        return state;
    }

    private static Stack<int> PopulatePlayed(List<int> played = null)
    {
        return played == null
            ? new Stack<int>()
            : new Stack<int>(played);
    }

    private static Stack<int> PopulateToPlay(List<int> toPlay)
    {
        toPlay.Reverse();
        return new Stack<int>(toPlay);
    }
}
