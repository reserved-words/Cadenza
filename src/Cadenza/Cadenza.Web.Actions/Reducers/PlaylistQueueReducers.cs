namespace Cadenza.Web.Actions.Reducers;

public static class PlaylistQueueReducers
{
    [ReducerMethod]
    public static PlaylistQueueState ReducePlaylistQueueUpdateRequest(PlaylistQueueState state, PlaylistQueueUpdateRequest action)
    {
        if (action.Definition == null)
        {
            return PlaylistQueueState.Init();
        }

        var allTracks = action.Definition.Tracks.ToList();

        var tracksToPlay = allTracks.Skip(action.Definition.StartIndex).ToList();
        var playedTracks = allTracks.Take(action.Definition.StartIndex).ToList();

        var toPlayStack = PopulateToPlay(tracksToPlay);
        var playedStack = PopulatePlayed(playedTracks);

        var playing = toPlayStack.Pop();

        return state with
        {
            IsLoading = false,
            Played = playedStack,
            ToPlay = toPlayStack,
            CurrentTrack = playing
        };
    }

    [ReducerMethod(typeof(PlaylistQueueMoveNextRequest))]
    public static PlaylistQueueState ReducePlaylistQueueMoveNextRequest(PlaylistQueueState state)
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

    [ReducerMethod(typeof(PlaylistQueueMovePreviousRequest))]
    public static PlaylistQueueState ReducePlaylistQueueMovePreviousRequest(PlaylistQueueState state)
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

    private static Stack<int> PopulatePlayed(List<int> played)
    {
        return new Stack<int>(played);
    }

    private static Stack<int> PopulateToPlay(List<int> toPlay)
    {
        toPlay.Reverse();
        return new Stack<int>(toPlay);
    }
}
