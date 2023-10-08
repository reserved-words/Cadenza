using Cadenza.Common.Http;
using Cadenza.Web.Info.Interfaces;

namespace Cadenza.Web.Info.Services;

internal class WebInfoHttpHelper : HttpHelper, IWebInfoHttpHelper
{
    public WebInfoHttpHelper(IHttpRequestSender sender)
            : base(HttpClientName.Database, sender)
    {
    }
}
