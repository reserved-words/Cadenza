using Cadenza.Common;
using System.Xml.Linq;

namespace Cadenza.LastFM;

public class LastFmClient : ILastFmClient
{
    private readonly IHasher _hasher;
    private readonly IHttpClient _httpClient;
    private readonly ILastFmConfig _config;


    public LastFmClient(IHttpClient httpClient, ILastFmConfig config, IHasher hasher)
    {
        _httpClient = httpClient;
        _config = config;
        _hasher = hasher;
    }

    public async Task<T> Get<T>(string url, Func<XElement, T> getValue)
    {
        var fullUrl = url
            .Add("api_key", _config.ApiKey)
            .Add("username", _config.Username);

        var response = await _httpClient.Get(fullUrl);

        var xml = await response.ToXml();

        return getValue(xml);
    }

    public async Task Post(string url, Dictionary<string, string> parameters)
    {
        var sessionKey = _config.SessionKey;

        parameters.Add("api_key", _config.ApiKey);
        parameters.Add("sk", sessionKey);

        var signature = await GetApiSignature(parameters);
        parameters.Add("api_sig", signature);

        await _httpClient.Post(url, parameters);
    }

    private async Task<string> GetApiSignature(Dictionary<string, string> parameters)
    {
        var signature = string.Join("", parameters.Keys
            .OrderBy(p => p)
            .Select(p => $"{p}{parameters[p]}"));

        signature += _config.ApiSecret;

        return await _hasher.MD5Hash(signature);
    }
}