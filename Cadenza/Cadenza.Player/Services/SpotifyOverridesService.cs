using System.Net.Http.Json;

namespace Cadenza.Player;

public class SpotifyOverridesService : IOverridesService
{
    private readonly IPlayerApiUrl _api;
    private readonly IHttpClient _httpClient;

    public SpotifyOverridesService(IHttpClient httpClient, IPlayerApiUrl api)
    {
        _httpClient = httpClient;
        _api = api;
    }

    private string AddOverrideUrl => _api.AddSpotifyOverride;
    private string GetOverridesUrl => _api.GetSpotifyOverrides;
    private string RemoveOverrideUrl => _api.RemoveSpotifyOverride;

    public async Task<bool> AddOverrides(List<MetaDataUpdate> overrides)
    {
        var response = await _httpClient.Post(AddOverrideUrl, null, overrides);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<MetaDataUpdate>> GetOverrides()
    {
        var response = await _httpClient.Get(GetOverridesUrl);
        if (!response.IsSuccessStatusCode)
            return new List<MetaDataUpdate>();

        return await response.Content.ReadFromJsonAsync<List<MetaDataUpdate>>();
    }

    public async Task<bool> RemoveOverride(string id, ItemProperty property)
    {
        var data = new { id, propertyName = property.ToString() };
        var response = await _httpClient.Delete(RemoveOverrideUrl, null, data);
        return response.IsSuccessStatusCode;
    }
}
