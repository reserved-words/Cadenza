namespace Cadenza.Web.Common.Interfaces;

public interface IChangeDetector
{
    bool HasAlbumChanged(UpdateAlbumVM originalAlbum, UpdateAlbumVM updatedAlbum);
    bool HasArtistChanged(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack);
    bool HaveAlbumTracksChanged(IReadOnlyCollection<UpdateAlbumTrackVM> tracks, IReadOnlyCollection<UpdateAlbumTrackVM> updatedTracks, out IReadOnlyCollection<UpdateAlbumTrackVM> changedTracks);
}