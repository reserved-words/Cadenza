namespace Cadenza.Database.Interfaces;

public interface IHistoryRepository
{
    Task<List<TopAlbumDTO>> GetTopAlbums(HistoryPeriod period, int maxItems);
    Task<List<TopArtistDTO>> GetTopArtists(HistoryPeriod period, int maxItems);
    Task<List<TopTrackDTO>> GetTopTracks(HistoryPeriod period, int maxItems);

    Task<List<RecentAlbumDTO>> GetRecentlyAddedAlbums(int maxItems);
    Task<List<RecentAlbumDTO>> GetRecentlyPlayedAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);

    Task ScrobbleTrack(int trackId, DateTime scrobbledAt);
    Task UpdateNowPlaying(int trackId, int secondsRemaining);

    Task<List<RecentTrackDTO>> GetRecentTracks(int maxItems);

    Task SyncScrobbles();
}