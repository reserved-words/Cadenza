using System.Net.Http.Json;

namespace Cadenza.Source.Spotify;

public class SpotifyApi : ISpotifyApi
{
    private readonly ISpotifyApiConfig _config;
    private readonly IHttpClient _httpClient;

    public SpotifyApi(IHttpClient httpClient, ISpotifyApiConfig config)
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

    public async Task<T> Get<T>(string url)
    {
        var authHeader = await GetAuthHeader();
        var response = await _httpClient.Get(url, authHeader);
        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task<string> GetAuthHeader()
    {
        var accessToken = await _config.AccessToken();
        var authHeader = $"Bearer {accessToken}";
        return authHeader;
    }
}
