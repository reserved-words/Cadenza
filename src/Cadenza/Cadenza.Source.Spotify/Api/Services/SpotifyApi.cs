using System.Net;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify;

public class SpotifyApi : ISpotifyApi
{
    private readonly ISpotifyApiConfig _config;
    private readonly IHttpHelper _httpClient;

    public SpotifyApi(IHttpHelper httpClient, ISpotifyApiConfig config)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task Put(string urlFormat, object data = null)
    {
        var deviceId = await _config.DeviceId();
        var url = string.Format(urlFormat, deviceId);
        var authHeader = await GetAuthHeader();
        await _httpClient.Put(url, authHeader, data);
    }

    public async Task<T> Get<T>(string url) where T : class
    {
        var authHeader = await GetAuthHeader();
        var response = await _httpClient.Get(url, authHeader);

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task<string> GetAuthHeader()
    {
        var accessToken = await _config.AccessToken();
        var authHeader = $"Bearer {accessToken}";
        return authHeader;
    }
}
