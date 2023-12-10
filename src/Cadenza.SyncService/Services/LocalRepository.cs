using Cadenza.Common.Utilities.Interfaces;
using Microsoft.Extensions.Options;

namespace Cadenza.SyncService.Services;

internal class LocalRepository : ISourceRepository
{
    private readonly IBase64Encoder _base64Encoder;
    private readonly ISyncHttpHelper _http;
    private readonly LocalApiSettings _apiSettings;

    public LocalRepository(ISyncHttpHelper http, IOptions<LocalApiSettings> apiSettings, IBase64Encoder base64Encoder)
    {
        _http = http;
        _apiSettings = apiSettings.Value;
        _base64Encoder = base64Encoder;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task<List<string>> GetAllTracks()
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracks}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<SyncTrackDTO> GetTrack(string idFromSource)
    {
        var idBase64 = _base64Encoder.Encode(idFromSource);
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTrack}/{idBase64}";
        return await _http.Get<SyncTrackDTO>(url);
    }

    public async Task RemoveTrack(TrackRemovalSyncDTO request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTrack}";
        await _http.Delete(url, request);
    }

    public async Task UpdateTracks(List<string> trackIdsFromSource, List<PropertyUpdateDTO> updates)
    {
        var data = new MultiTrackUpdatesDTO
        {
            TrackIds = trackIdsFromSource,
            Updates = updates
        };
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.UpdateTracks}";
        await _http.Post(url, data);
    }
}
