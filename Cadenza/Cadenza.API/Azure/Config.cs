using Cadenza.Azure;

namespace Cadenza.API.Azure;

public class Config : IAzureConfig
{
    private readonly IConfiguration _config;

    public Config(IConfiguration config)
    {
        _config = config;
    }

    public string AddSpotifyOverrideUrl => _config.GetSection("Azure").GetValue<string>("AddSpotifyOverrideUrl");
    public string GetSpotifyOverridesUrl => _config.GetSection("Azure").GetValue<string>("GetSpotifyOverridesUrl");
    public string RemoveSpotifyOverrideUrl => _config.GetSection("Azure").GetValue<string>("RemoveSpotifyOverrideUrl");
}
