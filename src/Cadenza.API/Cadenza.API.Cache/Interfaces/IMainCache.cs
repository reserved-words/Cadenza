namespace Cadenza.API.Cache.Interfaces;

internal interface IMainCache
{
    void CacheAlbum(AlbumDetailsDTO album);
    void CacheAlbumTrack(AlbumTrackLinkDTO albumTracks);
    void CacheArtist(ArtistDetailsDTO album);
    void CacheTrack(TrackDetailsDTO track);
    void Clear();
    AlbumDetailsDTO GetAlbum(int id);
    AlbumTrackLinkDTO GetAlbumTrack(int trackId);
    ArtistDetailsDTO GetArtist(int id);
    TrackDetailsDTO GetTrack(int id);
    TrackFullDTO GetFullTrack(int id);
}
