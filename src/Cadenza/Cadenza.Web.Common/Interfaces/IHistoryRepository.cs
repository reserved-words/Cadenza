namespace Cadenza.Web.Common.Interfaces;

public interface IHistoryRepository
{
    Task<List<RecentTrackVM>> GetRecentTracks(int maxItems);
    Task<List<RecentAlbumVM>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);
    Task<List<TopAlbumVM>> GetTopAlbums(HistoryPeriod period, int limit);
    Task<List<TopArtistVM>> GetTopArtists(HistoryPeriod period, int limit);
    Task<List<TopTrackVM>> GetTopTracks(HistoryPeriod period, int limit);
}