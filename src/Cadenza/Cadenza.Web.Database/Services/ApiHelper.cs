using Cadenza.Common.Utilities.Http;

namespace Cadenza.Web.Database.Services;

internal class ApiHttpHelper : HttpHelper, IApiHttpHelper
{
    public ApiHttpHelper(IJsonService jsonConverter, IHttpRequestSender sender)
        :base(HttpClientName.Database, jsonConverter, sender)
    {
    }

    public async Task<T> Get<T>(string url, object id) where T : new()
    {
        var urlWithId = $"{url}/{id}";
        return await Get<T>(urlWithId);
    }
}