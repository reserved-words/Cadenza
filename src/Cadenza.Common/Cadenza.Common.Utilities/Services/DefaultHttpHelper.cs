using Cadenza.Common.Domain.Enums;

namespace Cadenza.Common.Utilities.Services;

internal class DefaultHttpHelper : HttpHelper
{
    public DefaultHttpHelper(IJsonConverter jsonConverter, IHttpRequestSender sender) 
        : base(HttpClientName.Default, jsonConverter, sender)
    {
    }
}
