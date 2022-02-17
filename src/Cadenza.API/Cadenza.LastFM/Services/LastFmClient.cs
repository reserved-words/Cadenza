using Cadenza.LastFM.Interfaces;
using Cadenza.LastFM.Model;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace Cadenza.LastFM.Services;

public class LastFmClient : ILastFmClient
{
    private readonly IHttpHelper _httpClient;
    private readonly IOptions<LastFmSettings> _config;

    public LastFmClient(IHttpHelper httpClient, IOptions<LastFmSettings> config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<T> Get<T>(string url, Func<XElement, T> getValue)
    {
        url = url.Add("api_key", _config.Value.ApiKey);

        var response = await _httpClient.Get(url);

        var xml = await response.ToXml();

        return getValue(xml);
    }
}
