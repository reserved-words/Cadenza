namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task RemoveTrack(TrackRemovalRequestDTO request);
    Task UpdateTrack(ItemUpdateRequestDTO updates);
    Task UpdateAlbum(ItemUpdateRequestDTO updates);
    Task UpdateArtist(ItemUpdateRequestDTO updates);
}
