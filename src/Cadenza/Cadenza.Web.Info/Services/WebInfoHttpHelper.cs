using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Utilities.Http;
using Cadenza.Common.Utilities.Interfaces;
using Cadenza.Web.Info.Interfaces;

namespace Cadenza.Web.Info.Services;

internal class WebInfoHttpHelper : HttpHelper, IWebInfoHttpHelper
{
    public WebInfoHttpHelper(IJsonService jsonConverter, IHttpRequestSender sender)
            : base(HttpClientName.Database, jsonConverter, sender)
    {
    }
}
