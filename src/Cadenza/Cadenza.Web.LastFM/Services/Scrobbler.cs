using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.LastFM.Services;

internal class Scrobbler : IPlayTracker
{
    private readonly IHttpHelper _http;
    private readonly IUrl _url;
    private readonly IAppStore _store;
    private readonly LastFmApiSettings _apiSettings;

    public Scrobbler(IUrl url, IHttpHelper http, IOptions<LastFmApiSettings> settings, IAppStore store)
    {
        _url = url;
        _http = http;
        _apiSettings = settings.Value;
        _store = store;
    }

    public async Task RecordPlay(TrackFull track, DateTime timestamp)
    {
        var scrobble = await GetScrobble(track, null, timestamp);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Scrobble);
        await _http.Post(url, null, scrobble);
    }

    public async Task UpdateNowPlaying(TrackFull track, int duration)
    {
        var scrobble = await GetScrobble(track, duration, null);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.UpdateNowPlaying);
        await _http.Post(url, null, scrobble);
    }

    private async Task<LFM_Scrobble> GetScrobble(TrackFull track, int? duration, DateTime? timestamp)
    {
        var sessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);

        return new LFM_Scrobble
        {
            SessionKey = sessionKey.Value,
            Timestamp = timestamp ?? DateTime.Now,
            Artist = track.Artist.Name,
            Title = track.Track.Title,
            AlbumTitle = track.Album.Title,
            AlbumArtist = track.Album.ArtistName,
            Duration = duration ?? track.Track.DurationSeconds
        };
    }
}
