namespace Cadenza.Library;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(string id);
    Task<List<AlbumTrack>> GetTracks(string albumId);
}
