namespace Cadenza.API.LastFM.Services;

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

    public async Task<T> Get<T>(string url, Func<XElement, T> getValue)
    {
        url = _urlService.AddParameter(url, "api_key", _config.Value.ApiKey);

        var response = await _httpClient.Get(url);

        var xml = _responseReader.GetXmlContent(response);

        return getValue(xml);
    }
}
