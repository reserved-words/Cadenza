using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Interfaces;

internal interface IPlayCache
{
    void CacheTrack(TrackInfo track, ArtistInfo artist, AlbumInfo album);
    void Clear();
    List<int> GetAll();
    List<int> GetTag(string id);
}
