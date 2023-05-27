using Cadenza.Common.Domain.Enums;

namespace Cadenza.Common.Interfaces.Utilities;

public interface IHttpRequestSender
{
    Task<HttpResponseMessage> TrySendRequest(HttpRequestMessage request, HttpClientName httpClientName = HttpClientName.Default);
}
