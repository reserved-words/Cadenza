namespace Cadenza.Web.Database.Services;

internal class ApiHttpHelper : HttpHelper, IApiHttpHelper
{
    public ApiHttpHelper(IHttpRequestSender sender)
        :base(HttpClientName.Database, sender)
    {
    }

    public async Task<T> Get<T>(string url, object id) where T : new()
    {
        var urlWithId = $"{url}/{id}";
        return await Get<T>(urlWithId);
    }
}