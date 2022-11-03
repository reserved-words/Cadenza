namespace Cadenza.API.Cache.Interfaces;

internal interface IHelperCache
{
    void CacheAlbum(AlbumInfo album);
    void CacheAlbumTrack(AlbumTrackLink albumTracks, TrackInfo track);
    void CacheArtist(ArtistInfo album);
    void CacheTrack(TrackInfo track);
    void Clear();
    List<Album> GetAlbumsByArtist(string id);
    List<AlbumTrack> GetAlbumTracks(string id);
    List<Artist> GetArtistsByGenre(string id);
    List<Artist> GetArtistsByGrouping(Grouping id);
    List<Track> GetArtistTracks(string id);


}
