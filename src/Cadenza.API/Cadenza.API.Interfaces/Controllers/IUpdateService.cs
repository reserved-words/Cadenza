using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.Interfaces.Controllers;

public interface IUpdateService
{
    Task<List<ItemUpdates>> GetQueuedUpdates();
    Task UpdateTrack(LibrarySource source, ItemUpdates updates);
    Task UpdateAlbum(LibrarySource source, ItemUpdates updates);
    Task UpdateArtist(ItemUpdates updates);
}
