namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateRepository
{
    Task UpdateAlbumTracks(int albumId, IReadOnlyCollection<AlbumTrackVM> originalTracks, IReadOnlyCollection<AlbumTrackVM> updatedTrack);
    Task UpdateAlbum(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    Task UpdateArtist(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    Task UpdateTrack(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack);
}