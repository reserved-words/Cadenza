using Cadenza.Common.Enums;

namespace Cadenza.API.Interfaces.LastFm;

public interface IHistory
{
    Task<List<PlayedAlbumDTO>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1);
    Task<List<PlayedArtistDTO>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1);
    Task<List<PlayedTrackDTO>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1);
    Task<List<RecentTrackDTO>> GetRecentTracks(int limit, int page = 1);
}