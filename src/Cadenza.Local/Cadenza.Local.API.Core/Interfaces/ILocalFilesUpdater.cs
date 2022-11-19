namespace Cadenza.Local.API.Core.Interfaces;

internal interface ILocalFilesUpdater
{
    Task UpdateArtist(string id, List<EditedProperty> updates);
    Task UpdateAlbum(string id, List<EditedProperty> updates);
    Task UpdateTrack(string id, List<EditedProperty> updates);
}
