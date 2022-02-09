namespace Cadenza.Library;

public interface ISourceTrackRepository
{
    LibrarySource Source { get; }
    Task<TrackFull> GetTrack(string id);
}