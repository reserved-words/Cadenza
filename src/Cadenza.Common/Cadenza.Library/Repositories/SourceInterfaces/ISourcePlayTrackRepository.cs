namespace Cadenza.Library;

public interface ISourcePlayTrackRepository : IPlayTrackRepository
{
    public LibrarySource Source { get; }
}
