namespace Cadenza.Common.Http;

internal class HttpRequestSender : IHttpRequestSender
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpRequestSender(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HttpResponseMessage> TrySendRequest(HttpRequestMessage request, HttpClientName httpClientName = HttpClientName.Default)
    {
        var httpClient = _httpClientFactory.CreateClient(httpClientName.ToString());
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