using Cadenza.Common.Http.Interfaces;
using Microsoft.Extensions.Options;

namespace Cadenza.Common.LastFm.Services;

internal class ApiClient : IApiClient
{
    private readonly IHttpHelper _httpClient;
    private readonly IOptions<LastFmApiSettings> _config;
    private readonly IResponseReader _responseReader;
    private readonly IUrlService _urlService;

    public ApiClient(IHttpHelper httpClient, IOptions<LastFmApiSettings> config, IUrlService urlService, IResponseReader responseReader)
    {
        _httpClient = httpClient;
        _config = config;
        _urlService = urlService;
        _responseReader = responseReader;
    }

    public async Task<T> Get<T>(Dictionary<string, string> parameters, Func<XElement, T> getValue)
    {
        parameters.Add("api_key", _config.Value.ApiKey);

        var url = _config.Value.ApiBaseUrl;

        foreach (var parameter in parameters)
        {
            _urlService.AddParameter(url, parameter.Key, parameter.Value);
        }

        var response = await _httpClient.Get(url);

        var xml = _responseReader.GetXmlContent(response);

        return getValue(xml);
    }
}
