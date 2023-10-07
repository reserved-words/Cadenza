namespace Cadenza.API.Interfaces.Library;

public interface IAlbumRepository
{
    Task<AlbumDetailsDTO> GetAlbum(int id);
    Task<List<AlbumTrackDTO>> GetAlbumTracks(int albumId);
}
