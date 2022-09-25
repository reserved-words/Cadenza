using Cadenza.Domain.Models;

namespace Cadenza.Local.API.Core.Interfaces;

internal interface ILocalFilesUpdater
{
    Task UpdateArtist(string id, List<ItemPropertyUpdate> updates);
    Task UpdateAlbum(string id, List<ItemPropertyUpdate> updates);
    Task UpdateTrack(string id, List<ItemPropertyUpdate> updates);
}
