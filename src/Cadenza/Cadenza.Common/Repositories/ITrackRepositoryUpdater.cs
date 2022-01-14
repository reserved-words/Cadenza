using Cadenza.Domain;

namespace Cadenza.Common;

public interface ITrackRepositoryUpdater : ITrackRepository
{
    Task AddTrack(PlayingTrack track);
    Task AddTrack(FullTrack track);
    Task AddTrack(AlbumTrackInfo track);
}
