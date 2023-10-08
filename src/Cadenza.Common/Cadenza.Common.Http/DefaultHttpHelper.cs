namespace Cadenza.Common.Http;

internal class DefaultHttpHelper : HttpHelper
{
    public DefaultHttpHelper(IHttpRequestSender sender)
        : base(HttpClientName.Default, sender)
    {
    }
}
