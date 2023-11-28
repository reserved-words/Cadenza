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

    public async Task<List<TopAlbumVM>> GetTopAlbums(HistoryPeriod period, int maxItems)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopAlbums, ("period", period), ("maxItems", maxItems));
        return await _http.Get<List<TopAlbumVM>>(url);
    }

    public async Task<List<TopArtistVM>> GetTopArtists(HistoryPeriod period, int maxItems)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopArtists, ("period", period), ("maxItems", maxItems));
        return await _http.Get<List<TopArtistVM>>(url);
    }

    public async Task<List<TopTrackVM>> GetTopTracks(HistoryPeriod period, int maxItems)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopTracks, ("period", period), ("maxItems", maxItems));
        return await _http.Get<List<TopTrackVM>>(url);
    }
}
