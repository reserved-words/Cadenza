using Cadenza.Common;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Azure;

public class SpotifyOverridesService : IOverridesService
{
    private readonly IOptions<AzureSettings> _config;
    private readonly IHttpClient _httpClient;

    public SpotifyOverridesService(IHttpClient httpClient, IOptions<AzureSettings> config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    private string AddOverrideUrl => _config.Value.AddSpotifyOverrideUrl;
    private string GetOverridesUrl => _config.Value.GetSpotifyOverridesUrl;
    private string RemoveOverrideUrl => _config.Value.RemoveSpotifyOverrideUrl;

    public async Task<bool> AddOverrides(List<MetaDataUpdate> overrides)
    {
        // TODO: Change to do all in one transaction

        foreach (var o in overrides)
        {
            var overrideData = new AzureOverride
            {
                id = o.Id,
                item = o.Item,
                itemType = o.ItemType.ToString(),
                propertyName = o.Property.ToString(),
                originalValue = o.OriginalValue,
                overrideValue = o.UpdatedValue
            };

            var response = await _httpClient.Post(AddOverrideUrl, null, overrideData);
            if (!response.IsSuccessStatusCode)
                return false;
        }

        return true;
    }

    public async Task<List<MetaDataUpdate>> GetOverrides()
    {
        var response = await _httpClient.Get(GetOverridesUrl);
        if (!response.IsSuccessStatusCode)
            return new List<MetaDataUpdate>();

        var test = await response.Content.ReadAsStringAsync();

        var overrides = await response.Content.ReadFromJsonAsync<List<AzureOverride>>();
        return overrides.Select(o => new MetaDataUpdate
        {
            Id = o.id,
            Item = o.item,
            ItemType = o.itemType.Parse<ItemType>(),
            Property = o.propertyName.Parse<ItemProperty>(),
            OriginalValue = o.originalValue,
            UpdatedValue = o.overrideValue
        })
        .ToList();
    }

    public async Task<bool> RemoveOverride(string id, ItemProperty property)
    {
        var data = new { id, propertyName = property.ToString() };
        var response = await _httpClient.Delete(RemoveOverrideUrl, null, data);
        return response.IsSuccessStatusCode;
    }

}
