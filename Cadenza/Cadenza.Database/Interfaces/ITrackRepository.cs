using Cadenza.Common;

namespace Cadenza.Database;

public interface ITrackRepository
{
    Task<PlayingTrack> GetSummary(LibrarySource source, string id);
}

public interface ITrackRepositoryUpdater : ITrackRepository
{
    Task AddTrack(TrackInfo track);
}
