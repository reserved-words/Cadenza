namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task RemoveTrack(TrackRemovalRequest request);
    Task UpdateTrack(ItemUpdateRequest updates);
    Task UpdateAlbum(ItemUpdateRequest updates);
    Task UpdateArtist(ItemUpdateRequest updates);
}
