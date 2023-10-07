namespace Cadenza.API.Cache.Interfaces;

internal interface IPlayCache
{
    void CacheTrack(TrackDetailsDTO track, ArtistDetailsDTO artist, AlbumDetailsDTO album);
    void Clear();
    List<int> GetAll();
    List<int> GetTag(string id);
}
