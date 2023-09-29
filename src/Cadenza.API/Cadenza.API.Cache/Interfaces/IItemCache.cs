using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Interfaces;

internal interface IItemCache
{
    void CacheAlbum(AlbumDetails album);
    void CacheArtist(ArtistDetails artist);
    void CacheTrack(TrackDetails track, AlbumDetails album);
    void Clear();
    List<PlayerItem> GetTag(string id);
    List<PlayerItem> GetTags();
    List<PlayerItem> GetItems(PlayerItemType type);
}
