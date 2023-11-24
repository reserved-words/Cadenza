namespace Cadenza.Database.Interfaces;

public interface IHistoryRepository
{
    Task<List<RecentAlbumDTO>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);
}