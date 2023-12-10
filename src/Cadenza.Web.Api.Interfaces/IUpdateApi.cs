namespace Cadenza.Web.Api.Interfaces;

public interface IUpdateApi
{
    Task UpdateAlbumTracks(int albumId, IReadOnlyCollection<AlbumDiscVM> originalTracks, IReadOnlyCollection<AlbumDiscVM> updatedTracks);
    Task UpdateAlbum(AlbumDetailsVM updatedAlbum);
    Task UpdateArtist(ArtistDetailsVM updatedArtist);
    Task UpdateTrack(TrackDetailsVM updatedTrack);
}