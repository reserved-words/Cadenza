using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Track;
using Cadenza.Domain.Models.Updates;
using Cadenza.SyncService.Interfaces;
using Cadenza.SyncService.Settings;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.SyncService.Repositories;

internal class LocalRepository : ISourceRepository
{
    private readonly IHttpHelper _http;
    private readonly LocalApiSettings _apiSettings;

    public LocalRepository(IHttpHelper http, IOptions<LocalApiSettings> apiSettings)
    {
        _http = http;
        _apiSettings = apiSettings.Value;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task<List<string>> GetAllTracks()
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracks}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTrack}/{id}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<TrackFull>();
    }

    public async Task UpdateTracks(List<string> trackIds, List<PropertyUpdate> updates)
    {
        var data = new MultiTrackUpdates
        {
            TrackIds = trackIds,
            Updates = updates
        };
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.UpdateTracks}";
        await _http.Post(url, null, data);
    }
}
