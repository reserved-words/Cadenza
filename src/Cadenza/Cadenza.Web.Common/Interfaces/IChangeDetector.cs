namespace Cadenza.Web.Common.Interfaces;

public interface IChangeDetector
{
    bool HasAlbumChanged(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    bool HasArtistChanged(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack);
    bool HaveAlbumTracksChanged(IReadOnlyCollection<AlbumTrackVM> tracks, IReadOnlyCollection<AlbumTrackVM> updatedTracks, out IReadOnlyCollection<AlbumTrackVM> changedTracks);
}