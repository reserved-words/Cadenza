using Cadenza.Common.Http.Interfaces;
using Cadenza.Common.Http.Services;
using Cadenza.Web.Common.Enums;

namespace Cadenza.Web.Source.Local.Services;

public class LocalHttpHelper : HttpHelper, ILocalHttpHelper
{
    public LocalHttpHelper(IHttpRequestSender sender)
        : base(sender, HttpClientName.Local.ToString())
    {
    }
}
