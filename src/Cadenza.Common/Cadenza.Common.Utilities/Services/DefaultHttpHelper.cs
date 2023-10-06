using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Utilities.Http;
using Cadenza.Common.Utilities.Interfaces;

namespace Cadenza.Common.Utilities.Services;

internal class DefaultHttpHelper : HttpHelper
{
    public DefaultHttpHelper(IJsonService jsonConverter, IHttpRequestSender sender) 
        : base(HttpClientName.Default, jsonConverter, sender)
    {
    }
}
