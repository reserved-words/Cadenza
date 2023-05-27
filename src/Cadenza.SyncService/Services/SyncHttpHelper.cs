using Cadenza.Common.Domain.Exceptions;
using Cadenza.Common.Interfaces.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.SyncService.Services;

internal class SyncHttpHelper : ISyncHttpHelper
{
    private readonly AuthSettings _settings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonConverter _jsonConverter;

    public SyncHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter, IOptions<AuthSettings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _jsonConverter = jsonConverter;
        _settings = settings.Value;
    }

    private HttpClient HttpClient => _httpClientFactory.CreateClient();

    public async Task Delete<T>(string url, T data)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url);
        await AddAuthToken(httpRequest);

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        var response = await HttpClient.SendAsync(httpRequest);
        await ValidateResponse(response);
    }

    public async Task<T> Get<T>(string url) where T : class, new()
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        await AddAuthToken(httpRequest);

        var response = await HttpClient.SendAsync(httpRequest);
        await ValidateResponse(response);

        var content = await response.Content.ReadAsStringAsync();
        return _jsonConverter.Deserialize<T>(content);
    }

    public async Task Post<T>(string url, T data)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        await AddAuthToken(httpRequest);

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        var response = await HttpClient.SendAsync(httpRequest);
        await ValidateResponse(response);
    }

    private async Task AddAuthToken(HttpRequestMessage request)
    {
        var token = await GetToken();
        request.Headers.Add("Authorization", $"Bearer {token}");
    }

    private async Task<string> GetToken()
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

        var response = await HttpClient.SendAsync(request);
        await ValidateResponse(response);

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = _jsonConverter.Deserialize<TokenResponse>(content);
        return tokenResponse.access_token;
    }

    private static async Task ValidateResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var responseContent = response.Content != null
            ? await response.Content.ReadAsStringAsync()
            : "";

        throw new HttpException(response.RequestMessage.RequestUri, response.StatusCode, responseContent);
    }
}
