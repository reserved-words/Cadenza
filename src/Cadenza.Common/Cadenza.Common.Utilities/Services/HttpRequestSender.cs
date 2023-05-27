using Cadenza.Common.Domain.Exceptions;

namespace Cadenza.Common.Utilities.Services;

internal class HttpRequestSender : IHttpRequestSender
{
    public async Task<HttpResponseMessage> TrySendRequest(HttpClient httpClient, HttpRequestMessage request)
    {
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