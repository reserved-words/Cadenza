using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.History;
using Cadenza.Utilities.Interfaces;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.LastFM.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

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
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<PlayedAlbum>>();
    }

    public async Task<List<PlayedArtist>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopArtists, ("period", period), ("limit", limit), ("page", page));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<PlayedArtist>>();
    }

    public async Task<List<PlayedTrack>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TopTracks, ("period", period), ("limit", limit), ("page", page));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<PlayedTrack>>();
    }

    public async Task<List<RecentTrack>> GetRecentTracks(int limit, int page = 1)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.RecentTracks, ("limit", limit), ("page", page));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<RecentTrack>>();
    }
}
