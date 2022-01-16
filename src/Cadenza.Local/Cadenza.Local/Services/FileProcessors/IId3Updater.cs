using Cadenza.Domain;

namespace Cadenza.Local;

public interface IId3Updater
{
    List<ItemPropertyUpdateResult> UpdateArtist(string id, List<ItemPropertyUpdate> updates);
    List<ItemPropertyUpdateResult> UpdateAlbum(string id, List<ItemPropertyUpdate> updates);
    List<ItemPropertyUpdateResult> UpdateTrack(string id, List<ItemPropertyUpdate> updates);
}
