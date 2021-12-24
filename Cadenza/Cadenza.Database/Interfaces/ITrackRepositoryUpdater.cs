using Cadenza.Common;

namespace Cadenza.Database;

public interface ITrackRepositoryUpdater : ITrackRepository
{
    Task AddTrack(PlayingTrack track);
    Task AddTrack(FullTrack track);
}
