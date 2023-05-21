using Cadenza.Common.Interfaces.Utilities;
using Cadenza.Common.Utilities.Services;
using Cadenza.Web.Info.Interfaces;

namespace Cadenza.Web.Info.Services;

internal class WebInfoHttpHelper : HttpHelper, IWebInfoHttpHelper
{
    public WebInfoHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter)
            : base(httpClientFactory, jsonConverter)
    {
    }

    protected override string ClientName => "External";
}
