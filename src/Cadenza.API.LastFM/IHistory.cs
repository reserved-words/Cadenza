using Cadenza.Domain;

namespace Cadenza.API.LastFM;

public interface IHistory
{
    Task<List<PlayedAlbum>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1);
    Task<List<PlayedArtist>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1);
    Task<List<PlayedTrack>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1);
    Task<List<RecentTrack>> GetRecentTracks(int limit, int page = 1);
}