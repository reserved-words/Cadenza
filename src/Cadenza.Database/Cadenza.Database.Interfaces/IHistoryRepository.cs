namespace Cadenza.Database.Interfaces;

public interface IHistoryRepository
{
    Task<List<RecentAlbumDTO>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);

    Task<List<NewScrobbleDTO>> GetNewScrobbles();
    Task MarkScrobbled(int scrobbleId);
    Task MarkScrobbleFailed(int scrobbleId);
    Task ScrobbleTrack(int trackId, string username, DateTime scrobbledAt);

    Task<List<NowPlayingUpdateDTO>> GetNowPlayingUpdates();
    Task MarkNowPlayingUpdated(int userId);
    Task MarkNowPlayingFailed(int userId);
    Task UpdateNowPlaying(string username, int trackId, int secondsRemaining);
}