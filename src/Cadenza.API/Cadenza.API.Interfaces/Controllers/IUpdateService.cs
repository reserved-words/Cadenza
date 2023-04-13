namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task<List<ItemUpdateRequest>> GetQueuedUpdateRequests();
    Task UpdateTrack(LibrarySource source, ItemUpdateRequest updates);
    Task UpdateAlbum(LibrarySource source, ItemUpdateRequest updates);
    Task UpdateArtist(ItemUpdateRequest updates);
}
