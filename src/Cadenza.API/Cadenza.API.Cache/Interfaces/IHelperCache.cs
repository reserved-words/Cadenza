namespace Cadenza.API.Cache.Interfaces;

internal interface IHelperCache
{
    void CacheAlbum(AlbumDetailsDTO album);
    void CacheAlbumFeaturingArtist(int artistId, AlbumDetailsDTO album);
    void CacheAlbumTracks(AlbumTracksDTO albumTracks);
    void CacheArtist(ArtistDetailsDTO album);
    void CacheTrack(TrackDetailsDTO track);
    void Clear();
    List<AlbumDTO> GetAlbumsByArtist(int id);
    List<AlbumDTO> GetAlbumsFeaturingArtist(int id);
    AlbumTracksDTO GetAlbumTracks(int id);
    List<ArtistDTO> GetArtistsByGenre(string id);
    List<ArtistDTO> GetArtistsByGrouping(int id);
}
