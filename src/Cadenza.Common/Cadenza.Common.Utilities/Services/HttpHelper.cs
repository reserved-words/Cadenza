using Cadenza.Common.Domain.Exceptions;
using System.Net.Http.Json;

namespace Cadenza.Common.Utilities.Services;

internal class DefaultHttpHelper : HttpHelper
{
    public DefaultHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter) : base(httpClientFactory, jsonConverter)
    {
    }

    protected override string ClientName => "External";
}

public abstract class HttpHelper : IHttpHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonConverter _jsonConverter;

    public HttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter)
    {
        _httpClientFactory = httpClientFactory;
        _jsonConverter = jsonConverter;
    }

    protected abstract string ClientName { get; }

    private HttpClient HttpClient => _httpClientFactory.CreateClient(ClientName);

    public async Task Delete(string url, object data)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url);

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        var response = await HttpClient.SendAsync(httpRequest);
        await ValidateResponse(response);
    }

    public async Task<T> Get<T>(string url) where T : new()
    {
        var content = await Get(url);
        return _jsonConverter.Deserialize<T>(content);
    }

    public async Task<string> Get(string url)
    {
        var response = await HttpClient.GetAsync(url);
        await ValidateResponse(response);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<ArtworkImage> GetImage(string url)
    {
        var response = await HttpClient.GetAsync(url);
        await ValidateResponse(response);

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
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequest.Content = new FormUrlEncodedContent(parameters);
        var response = await HttpClient.SendAsync(httpRequest);
        await ValidateResponse(response);
    }

    public async Task Post(string url, object data)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        var response = await HttpClient.SendAsync(httpRequest);
        await ValidateResponse(response);
    }

    public async Task Put(string url, object data)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Put, url);

        if (data != null)
        {
            httpRequest.Content = JsonContent.Create(data);
        }

        var response = await HttpClient.SendAsync(httpRequest);
        await ValidateResponse(response);
    }

    public async Task ValidateResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var responseContent = await response.Content.ReadAsStringAsync();

        throw new HttpException(response.RequestMessage.RequestUri, response.StatusCode, responseContent);
    }
}