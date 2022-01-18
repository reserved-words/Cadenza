using Cadenza.Domain;

namespace Cadenza.Core;

public interface ITrackRepositoryUpdater : ITrackRepository
{
    Task AddTrack(PlayingTrack track);
    Task AddTrack(FullTrack track);
    Task AddTrack(AlbumTrackInfo track);
}
