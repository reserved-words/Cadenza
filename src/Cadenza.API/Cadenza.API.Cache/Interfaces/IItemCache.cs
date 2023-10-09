namespace Cadenza.API.Cache.Interfaces;

internal interface IItemCache
{
    void CacheAlbum(AlbumDetailsDTO album);
    void CacheArtist(ArtistDetailsDTO artist);
    void CacheTrack(TrackDetailsDTO track, AlbumDetailsDTO album);
    void Clear();
    List<PlayerItemDTO> GetTag(string id);
    List<PlayerItemDTO> GetTags();
    List<PlayerItemDTO> GetItems(PlayerItemType type);
}
