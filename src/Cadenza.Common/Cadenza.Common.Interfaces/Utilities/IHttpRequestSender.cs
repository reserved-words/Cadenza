namespace Cadenza.Common.Interfaces.Utilities;

public interface IHttpRequestSender
{
    Task<HttpResponseMessage> TrySendRequest(HttpClient httpClient, HttpRequestMessage request);
}
