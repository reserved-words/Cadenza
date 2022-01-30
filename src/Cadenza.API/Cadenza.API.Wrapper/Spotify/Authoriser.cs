using Cadenza.API.Core;
using Cadenza.API.Core.Spotify;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Web;

namespace Cadenza.API.Wrapper.Spotify;

public class Authoriser : IAuthoriser
{
    private readonly Api _api;
    private readonly IHttpHelper _http;

    public Authoriser(IOptions<Api> api, IHttpHelper http)
    {
        _api = api.Value;
        _http = http;
    }

    public async Task<string> GetAuthHeader()
    {
        var url = _api.Endpoints.SpotifyAuthHeader;
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var url = _api.Endpoints.SpotifyAuthUrl + HttpUtility.UrlEncode(redirectUri);
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetTokenUrl()
    {
        var url = _api.Endpoints.SpotifyTokenUrl;
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }
}
