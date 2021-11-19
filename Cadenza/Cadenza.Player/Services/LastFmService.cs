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

    public async Task RecordPlay(TrackSummary track, DateTime timestamp)
    {
        var model = await GetScrobble(track, null, timestamp);
        await _httpClient.Post(_api.Scrobble, null, model);
    }

    public async Task UpdateNowPlaying(TrackSummary track, int duration)
    {
        var model = await GetScrobble(track, duration, null);
        await _httpClient.Post(_api.UpdateNowPlaying, null, model);
    }

    public async Task<bool> IsFavourite(TrackSummary track)
    {
        var url = _api.IsFavourite;
        url = $"{url}?artist={track.Artist.Name}";
        url = $"{url}&title={track.Track.Title}";
        var response = await _httpClient.Get(url);
        var isFavourite = await response.Content.ReadFromJsonAsync<bool>();
        return isFavourite;
    }

    public async Task Favourite(TrackSummary track)
    {
        var model = GetTrack(track);
        await _httpClient.Post(_api.Favourite, null, model);
    }

    public async Task Unfavourite(TrackSummary track)
    {
        var model = GetTrack(track);
        await _httpClient.Post(_api.Unfavourite, null, model);
    }

    public async Task<object> GetTrack(TrackSummary track)
    {
        var sessionKey = await _store.GetValue(StoreKey.LastFmSessionKey);

        return new
        {
            SessionKey = sessionKey,
            Artist = track.Artist.Name,
            Title = track.Track.Title
        };
    }

    public async Task<object> GetScrobble(TrackSummary track, int? duration, DateTime? timestamp)
    {
        var sessionKey = await _store.GetValue(StoreKey.LastFmSessionKey);

        return new
        {
            SessionKey = sessionKey,
            Timestamp = timestamp ?? DateTime.Now,
            Artist = track.Artist.Name,
            Title = track.Track.Title,
            AlbumTitle = track.Album.Title,
            AlbumArtist = track.Album.ArtistName,
            Duration = duration ?? track.Track.DurationSeconds
        };
    }
}
