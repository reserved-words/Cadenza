namespace Cadenza.Library;

public interface ISourceAlbumRepository : IAlbumRepository
{
    LibrarySource Source { get; }
}