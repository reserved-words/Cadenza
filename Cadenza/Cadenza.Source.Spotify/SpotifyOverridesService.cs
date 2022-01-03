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

    private string AddOverrideUrl => GetEndpoint(s => s.AddOverride);

    private string GetOverridesUrl => GetEndpoint(s => s.GetOverrides);

    private string RemoveOverrideUrl => GetEndpoint(s => s.RemoveOverride);

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

    private string GetEndpoint(Func<SpotifyOverridesSettings.SpotifyOverridesEndpoints, string> getEndpoint)
    {
        var baseUrl = _settings.Value.BaseUrl;
        var endpoint = getEndpoint(_settings.Value.Endpoints);
        return $"{baseUrl}{endpoint}";
    }
}
