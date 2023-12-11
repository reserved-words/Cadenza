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

        return state with 
        { 
            Track = state.Track with
            {
                Album = action.UpdatedAlbum,
                AlbumTrack = state.Track.AlbumTrack with
                {
                    DiscCount = action.UpdatedAlbum.DiscCount
                }
            } 
        };
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

    [ReducerMethod]
    public static CurrentTrackState ReduceAlbumTracksUpdatedAction(CurrentTrackState state, AlbumTracksUpdatedAction action)
    {
        if (state.Track == null)
            return state;

        var updatedTrack = action.UpdatedAlbumTracks.SingleOrDefault(t => t.TrackId == state.Track.Id);

        if (updatedTrack == null)
            return state;

        return state with
        {
            Track = state.Track with
            {
                Track = state.Track.Track with
                {
                    Title = updatedTrack.Title                     
                },
                AlbumTrack = state.Track.AlbumTrack with
                {
                    TrackNo = updatedTrack.TrackNo,
                    DiscNo = updatedTrack.DiscNo,
                    TrackCount = updatedTrack.DiscTrackCount
                }
            }
        };
    }

    [ReducerMethod]
    public static CurrentTrackState ReduceArtistReleasesUpdatedAction(CurrentTrackState state, ArtistReleasesUpdatedAction action)
    {
        if (state.Track == null)
            return state;

        var updatedAlbum = action.UpdatedArtistReleases.SingleOrDefault(a => a.Id == state.Track.Album.Id);

        if (updatedAlbum == null)
            return state;

        return state with
        {
            Track = state.Track with
            {
                Album = state.Track.Album with
                {
                    Title = updatedAlbum.Title,
                    ReleaseType = updatedAlbum.ReleaseType,
                    Year = updatedAlbum.Year
                }
            }
        };
    }
}
