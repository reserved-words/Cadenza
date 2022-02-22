namespace Cadenza.Library;

public interface ISourcePlaylistRepository : IPlaylistRepository
{
    LibrarySource Source { get; }
}