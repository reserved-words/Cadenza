namespace Cadenza.Web.LastFM.Services;

internal class History : IPlayHistory
{
    private readonly IUrl _url;
    private readonly ILastFmHttpHelper _http;
    private readonly LastFmApiSettings _apiSettings;

    public History(IUrl url, ILastFmHttpHelper http, IOptions<LastFmApiSettings> apiSettings)
    {
        _http = http;
        _url = url;
        _apiSettings = apiSettings.Value;
    }

    public async Task<List<PlayedAlbumVM>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopAlbums, ("period", period), ("limit", limit), ("page", page));
        return await _http.Get<List<PlayedAlbumVM>>(url);
    }

    public async Task<List<PlayedArtistVM>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopArtists, ("period", period), ("limit", limit), ("page", page));
        return await _http.Get<List<PlayedArtistVM>>(url);
    }

    public async Task<List<PlayedTrackVM>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopTracks, ("period", period), ("limit", limit), ("page", page));
        return await _http.Get<List<PlayedTrackVM>>(url);
    }
}
