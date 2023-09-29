using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Interfaces;

internal interface IHelperCache
{
    void CacheAlbum(AlbumDetails album);
    void CacheAlbumFeaturingArtist(int artistId, AlbumDetails album);
    void CacheAlbumTrack(AlbumTrackLink albumTracks, TrackDetails track);
    void CacheArtist(ArtistDetails album);
    void CacheTrack(TrackDetails track);
    void Clear();
    List<Album> GetAlbumsByArtist(int id);
    List<Album> GetAlbumsFeaturingArtist(int id);
    List<AlbumTrack> GetAlbumTracks(int id);
    List<Artist> GetArtistsByGenre(string id);
    List<Artist> GetArtistsByGrouping(int id);
    List<Track> GetArtistTracks(int id);
}
