using Cadenza.API.LastFM;
using Cadenza.API.LastFM.Interfaces;
using Cadenza.API.LastFM.Model;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace Cadenza.API.LastFM.Services;

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
