namespace Cadenza.API.Cache.Interfaces;

internal interface IHelperCache
{
    void CacheAlbum(AlbumInfo album);
    void CacheAlbumFeaturingArtist(int artistId, AlbumInfo album);
    void CacheAlbumTrack(AlbumTrackLink albumTracks, TrackInfo track);
    void CacheArtist(ArtistInfo album);
    void CacheTrack(TrackInfo track);
    void Clear();
    List<Album> GetAlbumsByArtist(int id);
    List<Album> GetAlbumsFeaturingArtist(int id);
    List<AlbumTrack> GetAlbumTracks(int id);
    List<Artist> GetArtistsByGenre(string id);
    List<Artist> GetArtistsByGrouping(Grouping id);
    List<Track> GetArtistTracks(int id);
}
