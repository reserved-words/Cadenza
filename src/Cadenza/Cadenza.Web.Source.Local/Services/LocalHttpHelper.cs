namespace Cadenza.Web.Source.Local.Services;

public class LocalHttpHelper : HttpHelper, ILocalHttpHelper
{
    public LocalHttpHelper(IHttpRequestSender sender)
        : base(HttpClientName.Local, sender)
    {
    }
}
