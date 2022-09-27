using Cadenza.Domain.Model.History;
using Cadenza.Domain.Model.LastFm;
using Track = Cadenza.Domain.Model.LastFm.Track;

namespace Cadenza.API.Interfaces.Controllers;

public interface ILastFmService
{
    Task<string> AuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
    Task Favourite(Track track);
    Task<bool> IsFavourite(string artist, string title);
    Task<List<RecentTrack>> RecentTracks(int limit, int page);
    Task RecordPlay(Scrobble scrobble);
    Task<List<PlayedAlbum>> TopAlbums(HistoryPeriod period, int limit, int page);
    Task<List<PlayedArtist>> TopArtists(HistoryPeriod period, int limit, int page);
    Task<List<PlayedTrack>> TopTracks(HistoryPeriod period, int limit, int page);
    Task Unfavourite(Track track);
    Task UpdateNowPlaying(Scrobble scrobble);
}