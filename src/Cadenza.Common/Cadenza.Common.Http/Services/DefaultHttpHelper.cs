using Cadenza.Common.Http.Interfaces;

namespace Cadenza.Common.Http.Services;

internal class DefaultHttpHelper : HttpHelper
{
    public DefaultHttpHelper(IHttpRequestSender sender)
        : base(sender)
    {
    }
}
