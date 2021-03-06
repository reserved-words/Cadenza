using Cadenza.Domain;

namespace Cadenza.Local.Common.Interfaces;

public interface ILocalFilesUpdater
{
    Task UpdateArtist(string id, List<ItemPropertyUpdate> updates);
    Task UpdateAlbum(string id, List<ItemPropertyUpdate> updates);
    Task UpdateTrack(string id, List<ItemPropertyUpdate> updates);
}
