namespace Cadenza.Local.API;

public interface IUpdateService
{
    Task<bool> UpdateAlbum(AlbumInfo album, List<ItemPropertyUpdate> updates);
    Task<bool> UpdateArtist(ArtistInfo artist, List<ItemPropertyUpdate> updates);
    Task<FileUpdateQueue> GetQueue();
    Task<bool> UpdateTrack(TrackInfo track, List<ItemPropertyUpdate> updates);
    Task Unqueue(ItemPropertyUpdate update);
}
