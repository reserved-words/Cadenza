using Cadenza.Common.DTO;

namespace Cadenza.Web.LastFM.Services;

internal class Scrobbler : IPlayTracker
{
    private readonly ILastFmHttpHelper _http;
    private readonly IUrl _url;
    private readonly LastFmApiSettings _apiSettings;

    public Scrobbler(IUrl url, ILastFmHttpHelper http, IOptions<LastFmApiSettings> settings)
    {
        _url = url;
        _http = http;
        _apiSettings = settings.Value;
    }

    public async Task RecordPlay(TrackFullVM track, DateTime timestamp)
    {
        var scrobble = GetScrobble(track, timestamp);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Scrobble);
        await _http.Post(url, scrobble);
    }

    public async Task UpdateNowPlaying(TrackFullVM track, int secondsRemaining)
    {
        var nowPlaying = GetNowPlaying(track, secondsRemaining);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.UpdateNowPlaying);
        await _http.Post(url, nowPlaying);
    }

    private NowPlayingDTO GetNowPlaying(TrackFullVM track, int secondsRemaining)
    {
        return new NowPlayingDTO
        {
            TrackId = track.Id,
            SecondsRemaining = secondsRemaining
        };
    }

    private ScrobbleDTO GetScrobble(TrackFullVM track, DateTime timestamp)
    {
        return new ScrobbleDTO
        {
            Timestamp = timestamp,
            TrackId = track.Id,
        };
    }
}
