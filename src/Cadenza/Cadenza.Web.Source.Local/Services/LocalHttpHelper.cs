using Cadenza.Common.Utilities.Http;
using Cadenza.Common.Utilities.Interfaces;
using Cadenza.Web.Source.Local.Interfaces;

namespace Cadenza.Web.Source.Local.Services;

public class LocalHttpHelper : HttpHelper, ILocalHttpHelper
{
    public LocalHttpHelper(IJsonService jsonConverter, IHttpRequestSender sender)
        : base(HttpClientName.Local, jsonConverter, sender)
    {
    }
}
