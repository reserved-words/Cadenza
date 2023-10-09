namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task RemoveTrack(TrackRemovalRequestDTO request);
    Task UpdateTrack(UpdateTrackDTO updates);
    Task UpdateAlbum(UpdateAlbumDTO updates);
    Task UpdateArtist(UpdateArtistDTO updates);
}
