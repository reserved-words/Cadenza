namespace Cadenza.Library;

public interface IMergedTrackRepository
{
    Task<TrackFull> GetTrack(LibrarySource source, string id);
}
