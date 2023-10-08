using System.Net.Http.Json;
using Cadenza.Common.Http.Interfaces;

namespace Cadenza.SyncService.Services;

internal class SyncHttpHelper : ISyncHttpHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpRequestSender _httpRequestSender;
    private readonly IApiTokenFetcher _tokenFetcher;

    public SyncHttpHelper(IHttpClientFactory httpClientFactory, IHttpRequestSender httpRequestSender, IApiTokenFetcher tokenFetcher)
    {
        _httpClientFactory = httpClientFactory;
        _httpRequestSender = httpRequestSender;
        _tokenFetcher = tokenFetcher;
    }

    public async Task Delete<T>(string url, T data)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        await AddAuthToken(request);

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        await _httpRequestSender.TrySendRequest(request);
    }

    public async Task<T> Get<T>(string url) where T : class, new()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        await AddAuthToken(request);

        var response = await _httpRequestSender.TrySendRequest(request);

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task Post<T>(string url, T data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        await AddAuthToken(request);

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        await _httpRequestSender.TrySendRequest(request);
    }

    private async Task AddAuthToken(HttpRequestMessage request)
    {
        var token = await _tokenFetcher.GetToken();
        request.Headers.Add("Authorization", $"Bearer {token}");
    }
}
