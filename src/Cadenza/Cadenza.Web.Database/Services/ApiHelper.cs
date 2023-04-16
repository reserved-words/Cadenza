namespace Cadenza.Web.Database.Services;

internal class ApiHelper : IApiHelper
{
    private readonly IOptions<DatabaseApiSettings> _settings;
    private readonly IHttpHelper _http;

    public ApiHelper(IHttpHelper http, IOptions<DatabaseApiSettings> settings)
    {
        _http = http;
        _settings = settings;
    }

    public async Task<T> Get<T>(string endpoint)
    {
        var url = GetApiEndpoint(endpoint);
        return await _http.Get<T>(url);
    }

    public async Task<T> Get<T>(string endpoint, int id)
    {
        var url = GetApiEndpoint(endpoint, id);
        return await _http.Get<T>(url);
    }

    public async Task<T> Get<T>(string endpoint, string id)
    {
        var url = GetApiEndpoint(endpoint, id);
        return await _http.Get<T>(url);
    }

    public async Task Post(string endpoint)
    {
        var url = GetApiEndpoint(endpoint);
        await _http.Post(url, null, null);
    }

    public async Task Post<T>(string endpoint, T data)
    {
        var url = GetApiEndpoint(endpoint);
        await _http.Post(url, null, data);
    }

    private string GetApiEndpoint(string endpoint)
    {
        return $"{_settings.Value.BaseUrl}{endpoint}";
    }

    private string GetApiEndpoint(string endpoint, object id)
    {
        return $"{GetApiEndpoint(endpoint)}?id={id}";
    }
}
