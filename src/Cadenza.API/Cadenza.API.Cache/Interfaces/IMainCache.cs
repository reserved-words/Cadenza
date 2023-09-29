using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Interfaces;

internal interface IMainCache
{
    void CacheAlbum(AlbumInfo album);
    void CacheAlbumTrack(AlbumTrackLink albumTracks);
    void CacheArtist(ArtistInfo album);
    void CacheTrack(TrackInfo track);
    void Clear();
    AlbumInfo GetAlbum(int id);
    AlbumTrackLink GetAlbumTrack(int trackId);
    List<Artist> GetAllArtists();
    ArtistInfo GetArtist(int id);
    TrackInfo GetTrack(int id);
    TrackFull GetFullTrack(int id);
}
