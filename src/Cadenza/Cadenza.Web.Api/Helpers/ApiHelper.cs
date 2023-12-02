using Cadenza.Common.Http.Interfaces;
using Cadenza.Common.Http.Services;
using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Common.Enums;

namespace Cadenza.Web.Api.Helpers;

internal class ApiHttpHelper : HttpHelper, IApiHttpHelper
{
    public ApiHttpHelper(IHttpRequestSender sender)
        : base(sender, HttpClientName.Database.ToString())
    {
    }

    public async Task<T> Get<T>(string url, object id) where T : new()
    {
        var urlWithId = $"{url}/{id}";
        return await Get<T>(urlWithId);
    }
}