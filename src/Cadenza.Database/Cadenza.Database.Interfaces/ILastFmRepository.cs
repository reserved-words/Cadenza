namespace Cadenza.Database.Interfaces;

public interface ILastFmRepository
{
    Task<List<LovedTrackUpdateDTO>> GetLovedTrackUpdates();
    Task MarkLovedTrackUpdated(int userId, int trackId);
    Task MarkLovedTrackFailed(int userId, int trackId);
    Task<List<NewScrobbleDTO>> GetNewScrobbles();
    Task MarkScrobbled(int scrobbleId);
    Task MarkScrobbleFailed(int scrobbleId);
    Task<List<NowPlayingUpdateDTO>> GetNowPlayingUpdates();
    Task MarkNowPlayingUpdated(int userId);
    Task MarkNowPlayingFailed(int userId);
}
