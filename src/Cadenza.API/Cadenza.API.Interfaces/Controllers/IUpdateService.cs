namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task UpdateAlbumTracks(UpdateAlbumTracksDTO request);
    Task UpdateTrack(UpdateTrackDTO updates);
    Task UpdateAlbum(UpdateAlbumDTO updates);
    Task UpdateArtist(UpdateArtistDTO updates);
}
