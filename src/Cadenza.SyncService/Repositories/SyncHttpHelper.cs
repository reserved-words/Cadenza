using Cadenza.Common.Domain.Exceptions;
using Cadenza.Common.Interfaces.Utilities;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Cadenza.SyncService.Repositories;

internal class SyncHttpHelper : ISyncHttpHelper
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonConverter _jsonConverter;

    public SyncHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _jsonConverter = jsonConverter;
        _configuration = configuration;
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

        var auth = _configuration.GetSection("Authentication");

        var clientId = auth.GetValue<string>("ClientId");
        var clientSecret = auth.GetValue<string>("ClientSecret");
        var audience = auth.GetValue<string>("Audience");
        var tokenEndpoint = auth.GetValue<string>("TokenEndpoint");

        var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);

        var data = new TokenRequest
        {
            client_id = clientId,
            client_secret = clientSecret,
            audience = audience,
            grant_type = "client_credentials"
        };

        request.Content = JsonContent.Create(data);

        var response = await HttpClient.SendAsync(request);
        await ValidateResponse(response);

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = _jsonConverter.Deserialize<TokenResponse>(content);
        return tokenResponse.access_token;
    }

    private class TokenRequest
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string audience { get; set; }
        public string grant_type { get; set; }
    }

    private class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
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
