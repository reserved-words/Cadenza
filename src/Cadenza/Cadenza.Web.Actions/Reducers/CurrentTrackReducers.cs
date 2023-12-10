namespace Cadenza.Web.Actions.Reducers;

public static class CurrentTrackReducers
{
    [ReducerMethod(typeof(FetchTrackRequest))]
    public static CurrentTrackState ReduceFetchCurrentTrackAction(CurrentTrackState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static CurrentTrackState ReduceFetchCurrentTrackResultAction(CurrentTrackState state, UpdateCurrentTrackAction action) => state with
    {
        IsLoading = false,
        Track = action.FullTrack,
        IsLastInPlaylist = action.IsLastTrackInPlaylist
    };

    [ReducerMethod]
    public static CurrentTrackState ReduceAlbumUpdatedAction(CurrentTrackState state, AlbumUpdatedAction action)
    {
        if (state.Track == null || state.Track.Album.Id != action.AlbumId)
            return state;

        var updatedAlbum = state.Track.Album with
        {
            Title = action.UpdatedAlbum.Title,
            ReleaseType = action.UpdatedAlbum.ReleaseType,
            Year = action.UpdatedAlbum.Year,
            DiscCount = action.UpdatedAlbum.DiscCount,
            ArtworkBase64 = action.UpdatedAlbum.ArtworkBase64,
            Tags = action.UpdatedAlbum.Tags
        };

        var track = state.Track with { Album = updatedAlbum };

        return state with { Track = track };
    }

    [ReducerMethod]
    public static CurrentTrackState ReduceArtistUpdatedAction(CurrentTrackState state, ArtistUpdatedAction action)
    {
        if (state.Track == null)
            return state;

        var trackArtist = state.Track.Artist;
        var albumArtist = state.Track.AlbumArtist;

        if (state.Track.Artist.Id == action.UpdatedArtist.Id)
        {
            trackArtist = action.UpdatedArtist;
        }

        if (state.Track.AlbumArtist.Id == action.UpdatedArtist.Id)
        {
            albumArtist = action.UpdatedArtist;
        }

        var updatedTrack = state.Track with { Artist = trackArtist, AlbumArtist = albumArtist };

        return state with { Track = updatedTrack };
    }

    [ReducerMethod]
    public static CurrentTrackState ReduceTrackUpdatedAction(CurrentTrackState state, TrackUpdatedAction action)
    {
        if (state.Track == null || state.Track.Id != action.UpdatedTrack.Id)
            return state;

        var track = state.Track with { Track = action.UpdatedTrack };

        return state with { Track = track };
    }
}
