using Cadenza.Common.Utilities.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.SyncService.Services;

internal class ApiTokenFetcher : IApiTokenFetcher
{
    private readonly AuthSettings _settings;
    private readonly IApiTokenCache _cache;
    private readonly IJsonService _jsonConverter;
    private readonly IHttpRequestSender _httpRequestSender;

    public ApiTokenFetcher(IJsonService jsonConverter, IOptions<AuthSettings> settings, IHttpRequestSender httpRequestSender, IApiTokenCache cache)
    {
        _jsonConverter = jsonConverter;
        _settings = settings.Value;
        _httpRequestSender = httpRequestSender;
        _cache = cache;
    }

    public async Task<string> GetToken()
    {
        var cachedToken = _cache.GetToken();

        if (cachedToken != null)
            return cachedToken;

        var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenEndpoint);

        var data = new TokenRequest
        {
            client_id = _settings.ClientId,
            client_secret = _settings.ClientSecret,
            audience = _settings.Audience,
            grant_type = "client_credentials"
        };

        request.Content = JsonContent.Create(data);

        var response = await _httpRequestSender.TrySendRequest(request);

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = _jsonConverter.Deserialize<TokenResponse>(content);

        _cache.CacheToken(tokenResponse);

        return tokenResponse.access_token;
    }
}
