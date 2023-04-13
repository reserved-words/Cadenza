namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task RemoveTrack(TrackRemovalRequest request);
    Task UpdateTrack(LibrarySource source, ItemUpdateRequest updates);
    Task UpdateAlbum(LibrarySource source, ItemUpdateRequest updates);
    Task UpdateArtist(ItemUpdateRequest updates);
}
