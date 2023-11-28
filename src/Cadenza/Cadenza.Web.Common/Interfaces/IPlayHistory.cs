namespace Cadenza.Web.Common.Interfaces;

public interface IPlayHistory
{
    Task<List<TopAlbumVM>> GetTopAlbums(HistoryPeriod period, int limit);
    Task<List<TopArtistVM>> GetTopArtists(HistoryPeriod period, int limit);
    Task<List<TopTrackVM>> GetTopTracks(HistoryPeriod period, int limit);
}