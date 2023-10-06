using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Utilities.Interfaces;
using Cadenza.Common.Utilities.Services;
using Cadenza.Web.Info.Interfaces;

namespace Cadenza.Web.Info.Services;

internal class WebInfoHttpHelper : HttpHelper, IWebInfoHttpHelper
{
    public WebInfoHttpHelper(IJsonConverter jsonConverter, IHttpRequestSender sender)
            : base(HttpClientName.Database, jsonConverter, sender)
    {
    }
}
