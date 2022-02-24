namespace Cadenza.Library;

public interface IMergedAlbumRepository
{
    Task<AlbumInfo> GetAlbum(LibrarySource source, string id);
    Task<List<AlbumTrack>> GetTracks(LibrarySource source, string albumId);
}