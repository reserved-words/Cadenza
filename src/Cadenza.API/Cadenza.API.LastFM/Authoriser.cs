using Cadenza.Common.Utilities.Exceptions;

namespace Cadenza.API.LastFM;

internal class Authoriser : IAuthoriser
{
    private readonly IHttpHelper _http;
    private readonly IOptions<LastFmApiSettings> _config;
    private readonly ISigner _signer;
    private readonly IParser _parser;
    private readonly IResponseReader _responseReader;
    private readonly IUrlService _urlService;

    public Authoriser(IOptions<LastFmApiSettings> config, ISigner signer, IHttpHelper http, IParser parser, IUrlService urlService, IResponseReader responseReader)
    {
        _config = config;
        _signer = signer;
        _http = http;
        _parser = parser;
        _urlService = urlService;
        _responseReader = responseReader;
    }

    public Task<string> GetAuthUrl(string redirectUri)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "api_key", _config.Value.ApiKey },
            { "cb", redirectUri}
        };

        var result = _urlService.AddParameters(_config.Value.AuthUrl, parameters);
        return Task.FromResult(result);
    }

    public async Task<string> CreateSession(string token)
    {
        try
        {
            var url = GetSessionKeyUrl(token);
            var response = await _http.Get(url);
            var xml = _responseReader.GetXmlContent(response);
            return _parser.Get(xml, "session", "key");
        }
        catch (HttpException)
        {
            return "";
        }
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

        return _urlService.AddParameters(_config.Value.ApiBaseUrl, parameters);
    }
}