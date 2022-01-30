using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.LastFM;

public class LastFmAuth : ILastFmAuth
{
    private readonly IHttpHelper _http;
    private readonly IOptions<LastFmSettings> _config;
    private readonly ILastFmSigner _signer;

    public LastFmAuth(IOptions<LastFmSettings> config, ILastFmSigner signer, IHttpHelper http)
    {
        _config = config;
        _signer = signer;
        _http = http;
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

    public async Task<string> CreateSession(string token)
    {
        var url = GetSessionKeyUrl(token);
        var response = await _http.Get(url);

        if (!response.IsSuccessStatusCode)
            return "";

        var xml = await response.ToXml();
        return xml.Get("key");
    }

    private string GetSessionKeyUrl(string token)
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