using Cadenza.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cadenza.Core.Utilities;

public class OverrideService : IOverridesService
{
    private readonly IConfiguration _config;
    private readonly IHttpHelper _httpHelper;

    public OverrideService(IHttpHelper httpHelper, IConfiguration config)
    {
        _httpHelper = httpHelper;
        _config = config;
    }

    public async Task<List<ItemPropertyUpdate>> GetOverrides()
    {
        var apiBaseUrl = _config.GetValue<string>("CoreApi:BaseUrl");
        var endpoint = _config.GetValue<string>("CoreApi:Endpoints:GetOverrides");
        var url = $"{apiBaseUrl}{endpoint}";
        return await _httpHelper.Get<List<ItemPropertyUpdate>>(url);
    }

    public async Task<bool> RemoveOverride(string id, ItemProperty property)
    {
        var apiBaseUrl = _config.GetValue<string>("CoreApi:BaseUrl");
        var endpoint = _config.GetValue<string>("CoreApi:Endpoints:Remove");
        var url = $"{apiBaseUrl}{endpoint}?id={id}&property={property}";
        var response = await _httpHelper.Delete(url);
        return response.IsSuccessStatusCode;
    }
}
