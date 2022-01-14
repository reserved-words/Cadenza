using System.Net.Http.Json;

namespace Cadenza.Utilities;

public class HttpHelper : IHttpHelper
{
    private readonly System.Net.Http.HttpClient _client;

    public HttpHelper(System.Net.Http.HttpClient client)
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

    public async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> parameters)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new FormUrlEncodedContent(parameters)
        };

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