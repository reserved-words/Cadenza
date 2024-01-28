using Cadenza.Common;
using Cadenza.Common.Http.Interfaces;
using System.Net.Http.Json;

namespace Cadenza.SyncService.Services;

internal class SyncHttpHelper : ISyncHttpHelper
{
    private readonly IHttpRequestSender _httpRequestSender;

    public SyncHttpHelper(IHttpRequestSender httpRequestSender)
    {
        _httpRequestSender = httpRequestSender;
    }

    public async Task Delete<T>(string url, T data)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);

        if (data != null)
        {
            request.Content = JsonContent.Create(data, options: JsonSerialization.Options);
        }

        await _httpRequestSender.TrySendRequest(request);
    }

    public async Task<T> Get<T>(string url) where T : class, new()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        var response = await _httpRequestSender.TrySendRequest(request);

        return await response.Content.ReadFromJsonAsync<T>(JsonSerialization.Options);
    }

    public async Task Post<T>(string url, T data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        if (data != null)
        {
            request.Content = JsonContent.Create(data, options: JsonSerialization.Options);
        }

        await _httpRequestSender.TrySendRequest(request);
    }
}
