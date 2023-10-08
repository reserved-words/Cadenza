namespace Cadenza.Common.Http;

public interface IHttpRequestSender
{
    Task<HttpResponseMessage> TrySendRequest(HttpRequestMessage request, HttpClientName httpClientName = HttpClientName.Default);
}
