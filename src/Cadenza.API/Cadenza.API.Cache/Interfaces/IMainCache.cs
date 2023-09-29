using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Interfaces;

internal interface IMainCache
{
    void CacheAlbum(AlbumDetails album);
    void CacheAlbumTrack(AlbumTrackLink albumTracks);
    void CacheArtist(ArtistDetails album);
    void CacheTrack(TrackDetails track);
    void Clear();
    AlbumDetails GetAlbum(int id);
    AlbumTrackLink GetAlbumTrack(int trackId);
    List<Artist> GetAllArtists();
    ArtistDetails GetArtist(int id);
    TrackDetails GetTrack(int id);
    TrackFull GetFullTrack(int id);
}
