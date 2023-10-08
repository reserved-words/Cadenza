using Cadenza.Common.Http.Interfaces;
using Cadenza.Common.Http.Services;
using Cadenza.Web.Common.Enums;
using Cadenza.Web.Info.Interfaces;

namespace Cadenza.Web.Info.Services;

internal class WebInfoHttpHelper : HttpHelper, IWebInfoHttpHelper
{
    public WebInfoHttpHelper(IHttpRequestSender sender)
            : base(sender, HttpClientName.Database.ToString())
    {
    }
}
