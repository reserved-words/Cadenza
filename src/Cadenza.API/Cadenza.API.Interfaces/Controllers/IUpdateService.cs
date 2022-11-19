namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task<List<EditedItem>> GetQueuedItemEdits();
    Task UpdateTrack(LibrarySource source, EditedItem editedItem);
    Task UpdateAlbum(LibrarySource source, EditedItem editedItem);
    Task UpdateArtist(EditedItem editedItem);
}
