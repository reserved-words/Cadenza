using Cadenza.Common.Domain.Model;
using Cadenza.State.Actions;
using Cadenza.State.Store;

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
            ? null
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
            if (playing != null)
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
        var playedTracks = new List<PlayTrack>(state.Played);
        var playedTrack = playedTracks.FirstOrDefault(t => t.Id == action.TrackId);

        if (playedTrack != null)
        {
            playedTracks.Remove(playedTrack);
            PopulatePlayed(playedTracks);
        }

        var toPlayTracks = new List<PlayTrack>(state.ToPlay);
        var toPlayTrack = toPlayTracks.FirstOrDefault(t => t.Id == action.TrackId);

        if (toPlayTrack != null)
        {
            toPlayTracks.Remove(toPlayTrack);
            PopulateToPlay(toPlayTracks);
        }

        return state;
    }

    private static Stack<PlayTrack> PopulatePlayed(List<PlayTrack> played = null)
    {
        return played == null
            ? new Stack<PlayTrack>()
            : new Stack<PlayTrack>(played);
    }

    private static Stack<PlayTrack> PopulateToPlay(List<PlayTrack> toPlay)
    {
        toPlay.Reverse();
        return new Stack<PlayTrack>(toPlay);
    }
}
