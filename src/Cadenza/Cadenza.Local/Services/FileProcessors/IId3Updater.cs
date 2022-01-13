namespace Cadenza.Local;

public interface IId3Updater
{
    List<MetaDataUpdateResult> UpdateArtist(string id, List<MetaDataUpdate> updates);
    List<MetaDataUpdateResult> UpdateAlbum(string id, List<MetaDataUpdate> updates);
    List<MetaDataUpdateResult> UpdateTrack(string id, List<MetaDataUpdate> updates);
}
