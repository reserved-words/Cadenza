using Cadenza.Common.Interfaces.Utilities;
using Microsoft.Extensions.Options;

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
        return await _http.Get<List<string>>(url);
    }

    public async Task<SyncTrack> GetTrack(string id)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTrack}/{id}";
        return await _http.Get<SyncTrack>(url);
    }

    public async Task RemoveTrack(RemoveTrackRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTrack}";
        await _http.Delete(url, null, request);
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
