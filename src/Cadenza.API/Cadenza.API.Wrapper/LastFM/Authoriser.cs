using Cadenza.API.Core;
using Cadenza.API.Core.LastFM;
using Cadenza.Utilities;

namespace Cadenza.API.Wrapper.LastFM;

internal class Authoriser : IAuthoriser
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;

    public Authoriser(IUrl url, IHttpHelper http)
    {
        _url = url;
        _http = http;
    }

    public async Task<string> CreateSession(string token)
    {
        var url = _url.Build(ApiEndpoints.LastFmCreateSession, ("token", token));
        return await _http.GetString(url);
    }

    public async Task<string> GetAuthUrl(string state, string redirectUri)
    {
        var url = _url.Build(ApiEndpoints.LastFmAuthUrl, ("state", state), ("redirectUri", redirectUri)); 
        return await _http.GetString(url);
    }
}
