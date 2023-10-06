using Cadenza.Common.Domain.Enums;

namespace Cadenza.Common.Utilities.Interfaces;

public interface IHttpRequestSender
{
    Task<HttpResponseMessage> TrySendRequest(HttpRequestMessage request, HttpClientName httpClientName = HttpClientName.Default);
}
