using Cadenza.Common.Enums;
using Cadenza.Common.LastFm;

namespace Cadenza.API.Interfaces.Controllers;

public interface ILastFmService
{
    Task<string> AuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
    Task Favourite(FavouriteTrack track);
    Task<bool> IsFavourite(string artist, string title);
    Task<List<RecentTrackDTO>> RecentTracks(int limit, int page);
    Task RecordPlay(Scrobble scrobble);
    Task<List<PlayedAlbumDTO>> TopAlbums(HistoryPeriod period, int limit, int page);
    Task<List<PlayedArtistDTO>> TopArtists(HistoryPeriod period, int limit, int page);
    Task<List<PlayedTrackDTO>> TopTracks(HistoryPeriod period, int limit, int page);
    Task Unfavourite(FavouriteTrack track);
    Task UpdateNowPlaying(Scrobble scrobble);
}
