using Cadenza.Domain;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify;

public class SpotifyOverridesService : IOverridesService
{
    private readonly IHttpHelper _httpClient;
    private readonly IOptions<SpotifyOverridesSettings> _settings;

    public SpotifyOverridesService(IOptions<SpotifyOverridesSettings> settings, IHttpHelper httpClient)
    {
        _settings = settings;
        _httpClient = httpClient;
    }

    private string AddOverrideUrl => _settings.GetApiEndpoint(s => s.AddOverride);

    private string GetOverridesUrl => _settings.GetApiEndpoint(s => s.GetOverrides);

    private string RemoveOverrideUrl => _settings.GetApiEndpoint(s => s.RemoveOverride);

    public async Task<bool> AddOverrides(List<ItemPropertyUpdate> overrides)
    {
        var response = await _httpClient.Post(AddOverrideUrl, null, overrides);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<ItemPropertyUpdate>> GetOverrides()
    {
        var response = await _httpClient.Get(GetOverridesUrl);
        if (!response.IsSuccessStatusCode)
            return new List<ItemPropertyUpdate>();

        return await response.Content.ReadFromJsonAsync<List<ItemPropertyUpdate>>();
    }

    public async Task<bool> RemoveOverride(string id, ItemProperty property)
    {
        var data = new { id, propertyName = property.ToString() };
        var response = await _httpClient.Delete(RemoveOverrideUrl, null, data);
        return response.IsSuccessStatusCode;
    }
}
