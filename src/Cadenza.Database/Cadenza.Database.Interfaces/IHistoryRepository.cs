namespace Cadenza.Database.Interfaces;

public interface IHistoryRepository
{
    Task<List<RecentAlbumDTO>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);

    Task ScrobbleTrack(int trackId, string username, DateTime scrobbledAt);
    Task UpdateNowPlaying(string username, int trackId, int secondsRemaining);

    Task<List<RecentTrackDTO>> GetRecentTracks(string username, int maxItems);

    Task SyncScrobbles();
}