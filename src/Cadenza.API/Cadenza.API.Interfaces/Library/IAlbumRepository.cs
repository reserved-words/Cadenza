namespace Cadenza.API.Interfaces.Library;

public interface IAlbumRepository
{
    Task<AlbumDetails> GetAlbum(int id);
    Task<List<AlbumTrack>> GetAlbumTracks(int albumId);
}
