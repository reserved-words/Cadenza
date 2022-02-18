namespace Cadenza.Library;

public interface IMergedAlbumRepository
{
    Task<AlbumInfo> GetAlbum(LibrarySource source, string id);
    Task<List<Track>> GetTracks(LibrarySource source, string albumId);
}