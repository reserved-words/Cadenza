using Cadenza.API.Core;
using Cadenza.API.Core.LastFM;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Web;

namespace Cadenza.API.Wrapper.LastFM;

public class Authoriser : IAuthoriser
{
    private readonly ApiSettings _api;
    private readonly IHttpHelper _http;

    public Authoriser(IOptions<ApiSettings> api, IHttpHelper http)
    {
        _api = api.Value;
        _http = http;
    }

    public async Task<string> CreateSession(string token)
    {
        var url = $"{GetApiUrl(ApiEndpoints.SpotifyAuthUrl)}?token={token}";
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var url = $"{GetApiUrl(ApiEndpoints.SpotifyAuthUrl)}?redirectUri={HttpUtility.UrlEncode(redirectUri)}";
        var response = await _http.Get(url);
        return await response.Content.ReadAsStringAsync();
    }

    private string GetApiUrl(string endpoint)
    {
        return $"{_api.BaseUrl}{endpoint}";
    }
}
