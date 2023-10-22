namespace Cadenza.API.Interfaces.Library;

public interface IAlbumRepository
{
    Task<AlbumDetailsDTO> GetAlbum(int id);
    Task<AlbumTracksDTO> GetAlbumTracks(int albumId);
}
