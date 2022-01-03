using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify;

public class SpotifyOverridesService : IOverridesService
{
    private readonly IHttpClient _httpClient;
    private readonly IOptions<SpotifyOverridesSettings> _settings;

    public SpotifyOverridesService(IOptions<SpotifyOverridesSettings> settings, IHttpClient httpClient)
    {
        _settings = settings;
        _httpClient = httpClient;
    }

    private string AddOverrideUrl => _settings.GetApiEndpoint(s => s.AddOverride);

    private string GetOverridesUrl => _settings.GetApiEndpoint(s => s.GetOverrides);

    private string RemoveOverrideUrl => _settings.GetApiEndpoint(s => s.RemoveOverride);

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
