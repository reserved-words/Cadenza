using Cadenza.Domain;

namespace Cadenza.Core;

public interface ITrackRepositoryUpdater : ITrackRepository
{
    Task AddTrack(TrackSummary track);
    Task AddTrack(TrackFull track);
    Task AddTrack(AlbumTrackInfo track);
}
