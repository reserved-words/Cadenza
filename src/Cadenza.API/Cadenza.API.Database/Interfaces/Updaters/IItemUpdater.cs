namespace Cadenza.API.Database.Interfaces.Updaters;

internal interface IItemUpdater
{
    void UpdateAlbum(AlbumInfo album, List<PropertyUpdate> updates);
    void UpdateArtist(ArtistInfo artist, List<PropertyUpdate> updates);
    void UpdateTrack(TrackInfo track, List<PropertyUpdate> updates);
}
