namespace Cadenza.API.Database.Interfaces.Updaters;

internal interface IItemUpdater
{
    void UpdateAlbum(AlbumInfo album, List<EditedProperty> updates);
    void UpdateArtist(ArtistInfo artist, List<EditedProperty> updates);
    void UpdateTrack(TrackInfo track, List<EditedProperty> updates);
}
