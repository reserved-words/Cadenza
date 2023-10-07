namespace Cadenza.Web.Common.Interfaces;

public interface IPlayHistory
{
    Task<List<PlayedAlbumVM>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1);
    Task<List<PlayedArtistVM>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1);
    Task<List<PlayedTrackVM>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1);
    Task<List<RecentTrackVM>> GetRecentTracks(int limit, int page = 1);
}