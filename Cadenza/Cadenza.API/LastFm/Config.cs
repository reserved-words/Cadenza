using Cadenza.LastFM;

namespace Cadenza.API.LastFm;

public class Config : ILastFmConfig
{
    private readonly IConfigurationSection _config;

    public Config(IConfiguration config)
    {
        _config = config.GetSection("LastFm");
    }

    public string ApiKey => _config.GetValue<string>("ApiKey");
    public string ApiSecret => _config.GetValue<string>("ApiSecret");
    public string Username => _config.GetValue<string>("Username");
    public string ApiBaseUrl => _config.GetValue<string>("ApiBaseUrl");
    public string AuthUrl => _config.GetValue<string>("AuthUrl");
}
