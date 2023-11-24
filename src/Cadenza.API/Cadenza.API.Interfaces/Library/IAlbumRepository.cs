namespace Cadenza.API.Interfaces.Library;

public interface IAlbumRepository
{
    Task<AlbumTracksDTO> GetAlbumTracks(int albumId);
}
