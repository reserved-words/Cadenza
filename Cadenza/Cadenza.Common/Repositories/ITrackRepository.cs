namespace Cadenza.Common;

public interface ITrackRepository
{
    Task<PlayingTrack?> GetSummary(LibrarySource source, string id);
    Task<FullTrack?> GetDetails(LibrarySource source, string id);
}
