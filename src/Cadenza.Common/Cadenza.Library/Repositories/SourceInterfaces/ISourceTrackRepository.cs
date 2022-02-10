namespace Cadenza.Library;

public interface ISourceTrackRepository : ITrackRepository
{
    LibrarySource Source { get; }
}