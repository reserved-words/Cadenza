using Cadenza.Common;

namespace Cadenza.Database;

public interface ITrackRepository
{
    Task<PlayingTrack?> GetSummary(LibrarySource source, string id);
    Task<FullTrack?> GetDetails(LibrarySource source, string id);
}

public interface ITrackRepositoryUpdater : ITrackRepository
{
    Task AddTrack(PlayingTrack track);
    Task AddTrack(FullTrack track);
}
