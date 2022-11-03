namespace Cadenza.API.Cache.Interfaces;

internal interface IPlayCache
{
    void CacheTrack(TrackInfo track, ArtistInfo artist, AlbumInfo album);
    void Clear();
    List<PlayTrack> GetAll();
    List<PlayTrack> GetTag(string id);
    PlayTrack GetTrack(string id);
}
