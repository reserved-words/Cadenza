using Cadenza.Core;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.LastFM;

public class Authoriser : IAuthoriser
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly LastFmApiSettings _apiSettings;

    public Authoriser(IUrl url, IHttpHelper http, IOptions<LastFmApiSettings> apiEndpoints)
    {
        _url = url;
        _http = http;
        _apiSettings = apiEndpoints.Value;
    }

    public async Task<string> CreateSession(string token)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.CreateSession, ("token", token));
        return await _http.GetString(url);
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AuthUrl, ("redirectUri", redirectUri));
        return await _http.GetString(url);
    }
}
