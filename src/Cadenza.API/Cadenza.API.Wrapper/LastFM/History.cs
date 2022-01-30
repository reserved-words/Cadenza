using Cadenza.API.Core;
using Cadenza.API.Core.LastFM;
using Cadenza.Domain;
using Cadenza.Utilities;
using System.Net.Http.Json;

namespace Cadenza.API.Wrapper.LastFM;

internal class History : IHistory
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;

    public History(IUrl url, IHttpHelper http)
    {
        _http = http;
        _url = url;
    }

    public async Task<List<PlayedAlbum>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(ApiEndpoints.TopAlbums, ("period", period), ("limit", limit), ("page", page));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<PlayedAlbum>>();
    }

    public async Task<List<PlayedArtist>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(ApiEndpoints.TopArtists, ("period", period), ("limit", limit), ("page", page));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<PlayedArtist>>();
    }

    public async Task<List<PlayedTrack>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1)
    {
        var url = _url.Build(ApiEndpoints.TopTracks, ("period", period), ("limit", limit), ("page", page));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<PlayedTrack>>();
    }

    public async Task<List<RecentTrack>> GetRecentTracks(int limit, int page = 1)
    {
        var url = _url.Build(ApiEndpoints.RecentTracks, ("limit", limit), ("page", page));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<RecentTrack>>();
    }
}
