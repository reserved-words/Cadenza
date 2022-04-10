using Cadenza.Domain;
using Cadenza.Local.Common.Model;

namespace Cadenza.Local.Common.Interfaces;

public interface IId3Updater
{
    Task<List<ItemPropertyUpdateResult>> UpdateArtist(string id, List<ItemPropertyUpdate> updates);
    Task<List<ItemPropertyUpdateResult>> UpdateAlbum(string id, List<ItemPropertyUpdate> updates);
    Task<List<ItemPropertyUpdateResult>> UpdateTrack(string id, List<ItemPropertyUpdate> updates);
}
