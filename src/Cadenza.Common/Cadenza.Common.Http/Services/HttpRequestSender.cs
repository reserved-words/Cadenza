using Cadenza.Common.Http.Interfaces;

namespace Cadenza.Common.Http.Services;

internal class HttpRequestSender : IHttpRequestSender
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpRequestSender(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HttpResponseMessage> TrySendRequest(HttpRequestMessage request, string httpClientName = null)
    {
        var httpClient = _httpClientFactory.CreateClient(httpClientName ?? HttpClientDefault.Name);
        var response = await httpClient.SendAsync(request);
        await ValidateResponse(response);
        return response;
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