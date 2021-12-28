namespace Cadenza.LastFM;

public class LastFmAuth : ILastFmAuth
{
    private readonly ILastFmConfig _config;
    private readonly ILastFmSigner _signer;

    public LastFmAuth(ILastFmConfig config, ILastFmSigner signer)
    {
        _config = config;
        _signer = signer;
    }

    public string GetAuthUrl(string redirectUri)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "api_key", _config.ApiKey },
            { "cb", redirectUri}
        };

        return _config.AuthUrl.Add(parameters);
    }

    public string GetSessionKeyUrl(string token)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "method", "auth.getSession" },
            { "api_key", _config.ApiKey },
            { "token", token}
        };

        _signer.Sign(parameters);

        return _config.ApiBaseUrl
            .Add(parameters);
    }
}