using Cadenza.Common.Utilities.Interfaces;
using Cadenza.Common.Utilities.Services;
using Cadenza.Web.Source.Local.Interfaces;

namespace Cadenza.Web.Source.Local.Services;

public class LocalHttpHelper : HttpHelper, ILocalHttpHelper
{
    public LocalHttpHelper(IJsonConverter jsonConverter, IHttpRequestSender sender)
        : base(HttpClientName.Local, jsonConverter, sender)
    {
    }
}
