namespace Cadenza.Web.LastFM.Services;

internal class History : IHistory
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly LastFmApiSettings _apiSettings;

    public History(IUrl url, IHttpHelper http, IOptions<LastFmApiSettings> apiSettings)
    {
        _http = http;
        _url = url;
        _apiSettings = apiSettings.Value;
    }

    public async Task<List<PlayedAlbum>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopAlbums, ("period", period), ("limit", limit), ("page", page));
        return await _http.Get<List<PlayedAlbum>>(url);
    }

    public async Task<List<PlayedArtist>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopArtists, ("period", period), ("limit", limit), ("page", page));
        return await _http.Get<List<PlayedArtist>>(url);
    }

    public async Task<List<PlayedTrack>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopTracks, ("period", period), ("limit", limit), ("page", page));
        return await _http.Get<List<PlayedTrack>>(url);
    }

    public async Task<List<RecentTrack>> GetRecentTracks(int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.RecentTracks, ("limit", limit), ("page", page));
        return await _http.Get<List<RecentTrack>>(url);
    }
}
