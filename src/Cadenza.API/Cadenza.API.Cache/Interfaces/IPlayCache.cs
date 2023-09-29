using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Interfaces;

internal interface IPlayCache
{
    void CacheTrack(TrackDetails track, ArtistDetails artist, AlbumDetails album);
    void Clear();
    List<int> GetAll();
    List<int> GetTag(string id);
}
