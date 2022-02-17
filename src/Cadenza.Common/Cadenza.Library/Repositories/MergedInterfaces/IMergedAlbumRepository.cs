namespace Cadenza.Library;

public interface IMergedAlbumRepository
{
    Task<AlbumInfo> GetAlbum(LibrarySource source, string id);
}