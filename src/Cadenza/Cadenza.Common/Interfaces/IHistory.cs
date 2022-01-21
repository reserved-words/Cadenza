using Cadenza.Domain;

namespace Cadenza.Common;

public interface IHistory
{
    Task<IEnumerable<RecentTrack>> GetRecentTracks(int limit, int page);
    Task<IEnumerable<PlayedTrack>> GetTopTracks(HistoryPeriod period, int limit, int page);
    Task<IEnumerable<PlayedAlbum>> GetTopAlbums(HistoryPeriod period, int limit, int page);
    Task<IEnumerable<PlayedArtist>> GetTopArtists(HistoryPeriod period, int limit, int page);
}