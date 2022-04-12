using Cadenza.Core;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Settings;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Spotify;

internal class SpotifyAuthHelper : IAuthHelper
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly SpotifyApiSettings _apiSettings;

    public SpotifyAuthHelper(IUrl url, IHttpHelper http, IOptions<SpotifyApiSettings> apiSettings)
    {
        _url = url;
        _http = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task<string> GetAuthHeader()
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AuthHeader);
        return await _http.GetString(url);
    }

    public async Task<string> GetAuthUrl(string state, string redirectUri)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AuthUrl, ("state", state), ("redirectUri", redirectUri));
        return await _http.GetString(url);
    }
}
