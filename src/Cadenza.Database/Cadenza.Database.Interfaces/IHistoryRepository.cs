namespace Cadenza.Database.Interfaces;

public interface IHistoryRepository
{
    Task<List<TopAlbumDTO>> GetTopAlbums(HistoryPeriod period, int maxItems);
    Task<List<TopArtistDTO>> GetTopArtists(HistoryPeriod period, int maxItems);
    Task<List<TopTrackDTO>> GetTopTracks(HistoryPeriod period, int maxItems);

    Task<List<RecentAlbumDTO>> GetRecentlyAddedAlbums(int maxItems);
    Task<List<RecentAlbumDTO>> GetRecentlyPlayedAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);

    Task ScrobbleTrack(int trackId, string username, DateTime scrobbledAt);
    Task UpdateNowPlaying(string username, int trackId, int secondsRemaining);

    Task<List<RecentTrackDTO>> GetRecentTracks(string username, int maxItems);

    Task SyncScrobbles();
}