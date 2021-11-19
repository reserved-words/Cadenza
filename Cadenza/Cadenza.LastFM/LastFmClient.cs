using Cadenza.Common;
using System.Xml.Linq;

namespace Cadenza.LastFM;

public class LastFmClient : ILastFmClient
{
    private readonly IHttpClient _httpClient;
    private readonly ILastFmConfig _config;

    public LastFmClient(IHttpClient httpClient, ILastFmConfig config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<T> Get<T>(string url, Func<XElement, T> getValue)
    {
        url = url
            .Add("api_key", _config.ApiKey)
            .Add("username", _config.Username);

        var response = await _httpClient.Get(url);

        var xml = await response.ToXml();

        return getValue(xml);
    }
}
