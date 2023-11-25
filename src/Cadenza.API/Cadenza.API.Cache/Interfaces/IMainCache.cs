namespace Cadenza.API.Cache.Interfaces;

internal interface IMainCache
{
    void CacheAlbum(AlbumDetailsDTO album);
    void CacheArtist(ArtistDetailsDTO album);
    void CacheTrack(TrackDetailsDTO track);
    void Clear();
    AlbumDetailsDTO GetAlbum(int id);
    ArtistDetailsDTO GetArtist(int id);
    TrackDetailsDTO GetTrack(int id);
}
