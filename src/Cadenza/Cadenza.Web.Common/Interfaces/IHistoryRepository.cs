namespace Cadenza.Web.Common.Interfaces;

public interface IHistoryRepository
{
    Task<List<RecentTrackVM>> GetRecentTracks(int maxItems);
    Task<List<RecentAlbumVM>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);
}