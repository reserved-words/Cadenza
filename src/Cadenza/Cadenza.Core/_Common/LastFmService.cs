using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Core;

public class LastFmService : IPlayTracker, IFavouritesConsumer, IFavouritesController, IHistory
{
    private readonly PlayerApiConfig _api;
    private readonly IHttpHelper _httpClient;
    private readonly IStoreGetter _store;

    public LastFmService(IHttpHelper httpClient, IStoreGetter store, IOptions<PlayerApiConfig> api)
    {
        _httpClient = httpClient;
        _store = store;
        _api = api.Value;
    }

    public async Task RecordPlay(TrackSummary track, DateTime timestamp)
    {
        var model = await GetScrobble(track, null, timestamp);
        await _httpClient.Post(Url(e => e.Scrobble), null, model);
    }

    public async Task UpdateNowPlaying(TrackSummary track, int duration)
    {
        var model = await GetScrobble(track, duration, null);
        await _httpClient.Post(Url(e => e.UpdateNowPlaying), null, model);
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        var url = Url(e => e.IsFavourite);
        url = $"{url}?artist={artist}";
        url = $"{url}&title={title}";
        var response = await _httpClient.Get(url);
        var isFavourite = await response.Content.ReadFromJsonAsync<bool>();
        return isFavourite;
    }

    public async Task Favourite(string artist, string title)
    {
        var model = await GetTrack(artist, title);
        await _httpClient.Post(Url(e => e.Favourite), null, model);
    }

    public async Task Unfavourite(string artist, string title)
    {
        var model = await GetTrack(artist, title);
        await _httpClient.Post(Url(e => e.Unfavourite), null, model);
    }

    public async Task<object> GetTrack(string artist, string title)
    {
        var sessionKey = await _store.GetString(StoreKey.LastFmSessionKey);

        return new
        {
            SessionKey = sessionKey,
            Artist = artist,
            Title = title
        };
    }

    public async Task<object> GetScrobble(TrackSummary track, int? duration, DateTime? timestamp)
    {
        var sessionKey = await _store.GetString(StoreKey.LastFmSessionKey);

        // Might be a better way to do this in future but for now omit album details for Spotify playlists
        return track.ReleaseType == ReleaseType.Playlist
            ? new
            {
                SessionKey = sessionKey,
                Timestamp = timestamp ?? DateTime.Now,
                Artist = track.Artist,
                Title = track.Title,
                Duration = duration ?? track.DurationSeconds
            }
            : new
            {
                SessionKey = sessionKey,
                Timestamp = timestamp ?? DateTime.Now,
                Artist = track.Artist,
                Title = track.Title,
                AlbumTitle = track.AlbumTitle,
                AlbumArtist = track.AlbumArtist,
                Duration = duration ?? track.DurationSeconds
            };
    }

    public async Task<IEnumerable<RecentTrack>> GetRecentTracks(int limit, int page)
    {
        var url = Url(e => e.RecentTracks);
        url = $"{url}?limit={limit}";
        url = $"{url}&page={page}";


        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<IEnumerable<RecentTrack>>();
    }

    public async Task<IEnumerable<PlayedTrack>> GetTopTracks(HistoryPeriod period, int limit, int page)
    {
        var url = Url(e => e.TopTracks);
        url = $"{url}?period={period}";
        url = $"{url}&limit={limit}";
        url = $"{url}&page={page}";
        var response = await _httpClient.Get(url);
        return await response.Content.ReadFromJsonAsync<IEnumerable<PlayedTrack>>();
    }

    public async Task<IEnumerable<PlayedAlbum>> GetTopAlbums(HistoryPeriod period, int limit, int page)
    {
        var url = Url(e => e.TopAlbums);
        url = $"{url}?period={period}";
        url = $"{url}&limit={limit}";
        url = $"{url}&page={page}";
        var response = await _httpClient.Get(url);
        return await response.Content.ReadFromJsonAsync<IEnumerable<PlayedAlbum>>();
    }

    public async Task<IEnumerable<PlayedArtist>> GetTopArtists(HistoryPeriod period, int limit, int page)
    {
        var url = Url(e => e.TopArtists);
        url = $"{url}?period={period}";
        url = $"{url}&limit={limit}";
        url = $"{url}&page={page}";
        var response = await _httpClient.Get(url);
        return await response.Content.ReadFromJsonAsync<IEnumerable<PlayedArtist>>();
    }

    private string Url(Func<PlayerApiEndpoints, string> getEndpoint)
    {
        return $"{_api.BaseUrl}{getEndpoint(_api.Endpoints)}";
    }
}
