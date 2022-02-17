namespace Cadenza.Local;

public interface IId3Updater
{
    Task<List<ItemPropertyUpdateResult>> UpdateArtist(string id, List<ItemPropertyUpdate> updates);
    Task<List<ItemPropertyUpdateResult>> UpdateAlbum(string id, List<ItemPropertyUpdate> updates);
    Task<List<ItemPropertyUpdateResult>> UpdateTrack(string id, List<ItemPropertyUpdate> updates);
}
