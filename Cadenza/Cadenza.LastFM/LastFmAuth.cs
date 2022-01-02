using Microsoft.Extensions.Options;

namespace Cadenza.LastFM;

public class LastFmAuth : ILastFmAuth
{
    private readonly IOptions<LastFmSettings> _config;
    private readonly ILastFmSigner _signer;

    public LastFmAuth(IOptions<LastFmSettings> config, ILastFmSigner signer)
    {
        _config = config;
        _signer = signer;
    }

    public string GetAuthUrl(string redirectUri)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "api_key", _config.Value.ApiKey },
            { "cb", redirectUri}
        };

        return _config.Value.AuthUrl.Add(parameters);
    }

    public string GetSessionKeyUrl(string token)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "method", "auth.getSession" },
            { "api_key", _config.Value.ApiKey },
            { "token", token}
        };

        _signer.Sign(parameters);

        return _config.Value.ApiBaseUrl
            .Add(parameters);
    }
}