namespace Cadenza.API.Cache.Interfaces;

internal interface IMainCache
{
    void CacheAlbum(AlbumInfo album);
    void CacheAlbumTrack(AlbumTrackLink albumTracks);
    void CacheArtist(ArtistInfo album);
    void CacheTrack(TrackInfo track);
    void Clear();
    AlbumInfo GetAlbum(int id);
    AlbumTrackLink GetAlbumTrack(string trackId);
    List<Artist> GetAllArtists();
    ArtistInfo GetArtist(int id);
    TrackInfo GetTrack(string id);
    TrackFull GetFullTrack(string id);
}
