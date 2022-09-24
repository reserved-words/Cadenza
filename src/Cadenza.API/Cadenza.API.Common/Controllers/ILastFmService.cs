using Cadenza.API.LastFM;
using Cadenza.Domain;

namespace Cadenza.API.Common.Controllers;

public interface ILastFmService
{
    Task<string> AuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
    Task Favourite(LastFM.Track track);
    Task<bool> IsFavourite(string artist, string title);
    Task<List<RecentTrack>> RecentTracks(int limit, int page);
    Task RecordPlay(Scrobble scrobble);
    Task<List<PlayedAlbum>> TopAlbums(HistoryPeriod period, int limit, int page);
    Task<List<PlayedArtist>> TopArtists(HistoryPeriod period, int limit, int page);
    Task<List<PlayedTrack>> TopTracks(HistoryPeriod period, int limit, int page);
    Task Unfavourite(LastFM.Track track);
    Task UpdateNowPlaying(Scrobble scrobble);
}