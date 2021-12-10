using System.Net.Http.Json;

namespace Cadenza.Player;

public class LastFmService : IPlayTracker, IFavouritesConsumer, IFavouritesController
{
    private readonly IPlayerApiUrl _api;
    private readonly IHttpClient _httpClient;
    private readonly IStoreGetter _store;

    public LastFmService(IHttpClient httpClient, IStoreGetter store, IPlayerApiUrl api)
    {
        _httpClient = httpClient;
        _store = store;
        _api = api;
    }

    public async Task RecordPlay(PlayingTrack track, DateTime timestamp)
    {
        var model = await GetScrobble(track, null, timestamp);
        await _httpClient.Post(_api.Scrobble, null, model);
    }

    public async Task UpdateNowPlaying(PlayingTrack track, int duration)
    {
        var model = await GetScrobble(track, duration, null);
        await _httpClient.Post(_api.UpdateNowPlaying, null, model);
    }

    public async Task<bool> IsFavourite(PlayingTrack track)
    {
        var url = _api.IsFavourite;
        url = $"{url}?artist={track.Artist}";
        url = $"{url}&title={track.Title}";
        var response = await _httpClient.Get(url);
        var isFavourite = await response.Content.ReadFromJsonAsync<bool>();
        return isFavourite;
    }

    public async Task Favourite(PlayingTrack track)
    {
        var model = GetTrack(track);
        await _httpClient.Post(_api.Favourite, null, model);
    }

    public async Task Unfavourite(PlayingTrack track)
    {
        var model = GetTrack(track);
        await _httpClient.Post(_api.Unfavourite, null, model);
    }

    public async Task<object> GetTrack(PlayingTrack track)
    {
        var sessionKey = await _store.GetValue(StoreKey.LastFmSessionKey);

        return new
        {
            SessionKey = sessionKey,
            Artist = track.Artist,
            Title = track.Title
        };
    }

    public async Task<object> GetScrobble(PlayingTrack track, int? duration, DateTime? timestamp)
    {
        var sessionKey = await _store.GetValue(StoreKey.LastFmSessionKey);

        return new
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
}
