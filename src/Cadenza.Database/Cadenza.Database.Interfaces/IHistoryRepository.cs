namespace Cadenza.Database.Interfaces;

public interface IHistoryRepository
{
    Task<List<NewScrobbleDTO>> GetNewScrobbles();
    Task<List<RecentAlbumDTO>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);
    Task MarkScrobbled(int scrobbleId);
    Task ScrobbleTrack(int trackId, string username, DateTime scrobbledAt);
}