using LFM_Track = Cadenza.Common.Domain.Model.LastFm.LFM_Track;

namespace Cadenza.API.Interfaces.Controllers;

public interface ILastFmService
{
    Task<string> AuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
    Task Favourite(LFM_Track track);
    Task<bool> IsFavourite(string artist, string title);
    Task<List<RecentTrack>> RecentTracks(int limit, int page);
    Task RecordPlay(LFM_Scrobble scrobble);
    Task<List<PlayedAlbum>> TopAlbums(HistoryPeriod period, int limit, int page);
    Task<List<PlayedArtist>> TopArtists(HistoryPeriod period, int limit, int page);
    Task<List<PlayedTrack>> TopTracks(HistoryPeriod period, int limit, int page);
    Task Unfavourite(LFM_Track track);
    Task UpdateNowPlaying(LFM_Scrobble scrobble);
}
