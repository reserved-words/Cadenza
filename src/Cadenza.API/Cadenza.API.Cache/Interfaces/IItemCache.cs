namespace Cadenza.API.Cache.Interfaces;

internal interface IItemCache
{
    void CacheAllbum(AlbumInfo album);
    void CacheArtist(ArtistInfo artist);
    void CacheTrack(TrackInfo track, AlbumInfo album);
    void Clear();
    List<PlayerItem> GetTag(string id);
    List<PlayerItem> GetTags();
    List<PlayerItem> GetItems(PlayerItemType type);
}
