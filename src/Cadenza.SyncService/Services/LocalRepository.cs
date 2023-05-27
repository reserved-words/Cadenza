using Microsoft.Extensions.Options;

namespace Cadenza.SyncService.Services;

internal class LocalRepository : ISourceRepository
{
    private readonly ISyncHttpHelper _http;
    private readonly LocalApiSettings _apiSettings;

    public LocalRepository(ISyncHttpHelper http, IOptions<LocalApiSettings> apiSettings)
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

    public async Task<SyncTrack> GetTrack(string idFromSource)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTrack}/{idFromSource}";
        return await _http.Get<SyncTrack>(url);
    }

    public async Task RemoveTrack(SyncTrackRemovalRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTrack}";
        await _http.Delete(url, request);
    }

    public async Task UpdateTracks(List<string> trackIdsFromSource, List<PropertyUpdate> updates)
    {
        var data = new MultiTrackUpdates
        {
            TrackIds = trackIdsFromSource,
            Updates = updates
        };
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.UpdateTracks}";
        await _http.Post(url, data);
    }
}
