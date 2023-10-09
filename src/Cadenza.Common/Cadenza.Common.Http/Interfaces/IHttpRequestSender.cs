namespace Cadenza.Common.Http.Interfaces;

public interface IHttpRequestSender
{
    Task<HttpResponseMessage> TrySendRequest(HttpRequestMessage request, string httpClientName = null);
}
