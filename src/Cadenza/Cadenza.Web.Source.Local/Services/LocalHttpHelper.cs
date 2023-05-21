
using Cadenza.Common.Utilities.Services;
using Cadenza.Web.Source.Local.Interfaces;

namespace Cadenza.Web.Source.Local.Services;

public class LocalHttpHelper : HttpHelper, ILocalHttpHelper
{
    public LocalHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter)
        : base(httpClientFactory, jsonConverter)
    {
    }

    protected override string ClientName => "LocalAPI";
}
