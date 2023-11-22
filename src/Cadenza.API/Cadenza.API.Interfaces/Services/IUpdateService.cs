namespace Cadenza.API.Interfaces.Services;

public interface IUpdateService
{
    Task UpdateAlbumTracks(UpdateAlbumTracksDTO request);
    Task UpdateTrack(UpdateTrackDTO updates);
    Task UpdateAlbum(UpdateAlbumDTO updates);
    Task UpdateArtist(UpdateArtistDTO updates);
}
