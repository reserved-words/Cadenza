using Cadenza.Common.Interfaces.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.SyncService.Services;

internal class ApiTokenFetcher : IApiTokenFetcher
{
    private readonly AuthSettings _settings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonConverter _jsonConverter;
    private readonly IHttpRequestSender _httpRequestSender;

    public ApiTokenFetcher(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter, IOptions<AuthSettings> settings, IHttpRequestSender httpRequestSender)
    {
        _httpClientFactory = httpClientFactory;
        _jsonConverter = jsonConverter;
        _settings = settings.Value;
        _httpRequestSender = httpRequestSender;
    }

    private HttpClient HttpClient => _httpClientFactory.CreateClient();

    public async Task<string> GetToken()
    {
        // TODO - cache token

        var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenEndpoint);

        var data = new TokenRequest
        {
            client_id = _settings.ClientId,
            client_secret = _settings.ClientSecret,
            audience = _settings.Audience,
            grant_type = "client_credentials"
        };

        request.Content = JsonContent.Create(data);

        var response = await _httpRequestSender.TrySendRequest(HttpClient, request);

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = _jsonConverter.Deserialize<TokenResponse>(content);
        return tokenResponse.access_token;
    }
}
