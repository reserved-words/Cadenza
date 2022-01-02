using Cadenza.Common;
using Microsoft.Extensions.Options;

namespace Cadenza.LastFM;

public class LastFmAuthorisedClient : ILastFmAuthorisedClient
{
    private readonly IHttpClient _httpClient;
    private readonly IOptions<LastFmSettings> _config;
    private readonly ILastFmSigner _signer;

    public LastFmAuthorisedClient(IHttpClient httpClient, IOptions<LastFmSettings> config, ILastFmSigner signer)
    {
        _httpClient = httpClient;
        _config = config;
        _signer = signer;
    }

    public async Task Post(string sessionKey, Dictionary<string, string> parameters)
    {
        parameters.Add("api_key", _config.Value.ApiKey);
        parameters.Add("sk", sessionKey);

        _signer.Sign(parameters);

        await _httpClient.Post(_config.Value.ApiBaseUrl, parameters);
    }
}
