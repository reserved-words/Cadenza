using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Utilities.Interfaces;
using System.Net.Http.Json;

namespace Cadenza.Common.Utilities.Services;

public abstract class HttpHelper : IHttpHelper
{
    private readonly HttpClientName _httpClientName;
    private readonly IHttpRequestSender _requestSender;
    private readonly IJsonConverter _jsonConverter;

    public HttpHelper(HttpClientName httpClientName, IJsonConverter jsonConverter, IHttpRequestSender requestSender)
    {
        _httpClientName = httpClientName;
        _jsonConverter = jsonConverter;
        _requestSender = requestSender;
    }

    public async Task Delete(string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        await Send(request);
    }

    public async Task<T> Get<T>(string url) where T : new()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await Send(request);
        var content = await response.Content.ReadAsStringAsync();
        return _jsonConverter.Deserialize<T>(content);
    }

    public async Task<string> Get(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await Send(request);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<ArtworkImage> GetImage(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await Send(request);

        var bytes = await response.Content.ReadAsByteArrayAsync();
        var mimeType = response.Content.Headers.ContentType.MediaType;

        if (!mimeType.StartsWith("image/"))
        {
            throw new Exception("Not an image URL");
        }

        return new ArtworkImage(bytes, mimeType);
    }

    public async Task Post(string url, Dictionary<string, string> parameters)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        request.Content = new FormUrlEncodedContent(parameters);

        await Send(request);
    }

    public async Task Post(string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        await Send(request);
    }

    public async Task Put(string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url);

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        await Send(request);
    }

    private async Task<HttpResponseMessage> Send(HttpRequestMessage request)
    {
        return await _requestSender.TrySendRequest(request, _httpClientName);
    }
}