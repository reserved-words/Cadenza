using Cadenza.Azure;

namespace Cadenza;

public class AzureConfig : IAzureConfig
{
    private readonly IConfiguration _config;

    public AzureConfig(IConfiguration config)
    {
        _config = config;
    }

    public string AddSpotifyOverrideUrl => _config.GetSection("Azure").GetValue<string>("AddSpotifyOverrideUrl");
    public string GetSpotifyOverridesUrl => _config.GetSection("Azure").GetValue<string>("GetSpotifyOverridesUrl");
    public string RemoveSpotifyOverrideUrl => _config.GetSection("Azure").GetValue<string>("RemoveSpotifyOverrideUrl");
}
