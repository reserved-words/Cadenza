namespace Cadenza.Library;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(string id);
    Task<List<Track>> GetTracks(string albumId);
}
