namespace Cadenza.Web.Api.Interfaces;

public interface IHistoryApi
{
    Task<List<RecentTrackVM>> GetRecentlyPlayedTracks(int maxItems);
    Task<List<RecentAlbumVM>> GetRecentlyAddedAlbums(int maxItems);
    Task<List<RecentAlbumVM>> GetRecentlyPlayedAlbums(int maxItems);
    Task<List<TopAlbumVM>> GetTopAlbums(HistoryPeriod period, int limit);
    Task<List<TopArtistVM>> GetTopArtists(HistoryPeriod period, int limit);
    Task<List<TopTrackVM>> GetTopTracks(HistoryPeriod period, int limit);
    Task RecordPlay(TrackFullVM track, DateTime timestamp);
    Task UpdateNowPlaying(TrackFullVM track, int secondsRemaining);
}