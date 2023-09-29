using Cadenza.Common.Domain.Model.Library;
using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.Web.LastFM.Services;

internal class Scrobbler : IPlayTracker
{
    private readonly ILastFmHttpHelper _http;
    private readonly IUrl _url;
    private readonly IState<LastFmConnectionState> _lastFmConnectionState;
    private readonly LastFmApiSettings _apiSettings;

    public Scrobbler(IUrl url, ILastFmHttpHelper http, IOptions<LastFmApiSettings> settings, IState<LastFmConnectionState> lastFmConnectionState)
    {
        _url = url;
        _http = http;
        _apiSettings = settings.Value;
        _lastFmConnectionState = lastFmConnectionState;
    }

    public async Task RecordPlay(TrackFull track, DateTime timestamp)
    {
        var scrobble = GetScrobble(track, null, timestamp);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Scrobble);
        await _http.Post(url, scrobble);
    }

    public async Task UpdateNowPlaying(TrackFull track, int duration)
    {
        var scrobble = GetScrobble(track, duration, null);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.UpdateNowPlaying);
        await _http.Post(url, scrobble);
    }

    private LFM_Scrobble GetScrobble(TrackFull track, int? duration, DateTime? timestamp)
    {
        var sessionKey = _lastFmConnectionState.Value.SessionKey;

        return new LFM_Scrobble
        {
            SessionKey = sessionKey,
            Timestamp = timestamp ?? DateTime.Now,
            Artist = track.Artist.Name,
            Title = track.Track.Title,
            AlbumTitle = track.Album.Title,
            AlbumArtist = track.Album.ArtistName,
            Duration = duration ?? track.Duration
        };
    }
}
