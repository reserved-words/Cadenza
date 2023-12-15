namespace Cadenza.Web.Common.Interfaces;

public interface IChangeDetector
{
    bool HasAlbumChanged(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    bool HasArtistChanged(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack);
    bool HaveAlbumTracksChanged(IReadOnlyCollection<AlbumTrackVM> originalTracks, IReadOnlyCollection<AlbumTrackVM> tracksAfterEdit, out List<AlbumTrackVM> changedTracks);
    bool HaveArtistReleasesChanged(IReadOnlyCollection<AlbumVM> originalReleases, IReadOnlyCollection<AlbumVM> releasesAfterEdit, out List<AlbumVM> changedReleases);
}