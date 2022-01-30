using Cadenza.API.Core;
using Cadenza.API.Core.Spotify;
using Cadenza.Utilities;

namespace Cadenza.API.Wrapper.Spotify;

internal class Authoriser : IAuthoriser
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;

    public Authoriser(IUrl url, IHttpHelper http)
    {
        _url = url;
        _http = http;
    }

    public async Task<string> GetAuthHeader()
    {
        var url = _url.Build(ApiEndpoints.SpotifyAuthHeader);
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var url = _url.Build(ApiEndpoints.SpotifyAuthUrl, ("redirectUri", redirectUri));
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetTokenUrl()
    {
        var url = _url.Build(ApiEndpoints.SpotifyTokenUrl);
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }
}
