using Cadenza.Utilities.Interfaces;
using System.Net.Http.Json;

namespace Cadenza.Utilities.Implementations;

public class HttpHelper : IHttpHelper
{
    private readonly HttpClient _client;

    public HttpHelper(HttpClient client)
    {
        _client = client;
    }

    public async Task<HttpResponseMessage> Post(string url, string authHeader = null, object data = null)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

        if (authHeader != null)
        {
            httpRequest.Headers.Add("Authorization", authHeader);
        }

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        return await _client.SendAsync(httpRequest);
    }

    public async Task<HttpResponseMessage> Post(string url, string authHeader = null, Dictionary<string, string> parameters = null)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

        if (parameters != null)
        {
            httpRequest.Content = new FormUrlEncodedContent(parameters);
        }

        if (authHeader != null)
        {
            httpRequest.Headers.Add("Authorization", authHeader);
        }

        return await _client.SendAsync(httpRequest);
    }

    public async Task<HttpResponseMessage> Put(string url, string authHeader = null, object data = null)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Put, url);

        if (authHeader != null)
        {
            httpRequest.Headers.Add("Authorization", authHeader);
        }

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        return await _client.SendAsync(httpRequest);
    }

    public async Task<HttpResponseMessage> Delete(string url, string authHeader = null, object data = null)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url);

        if (authHeader != null)
        {
            httpRequest.Headers.Add("Authorization", authHeader);
        }

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        return await _client.SendAsync(httpRequest);
    }

    public async Task<HttpResponseMessage> Get(string url, string authHeader = null)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

        if (authHeader != null)
        {
            httpRequest.Headers.Add("Authorization", authHeader);
        }

        return await _client.SendAsync(httpRequest);
    }
}