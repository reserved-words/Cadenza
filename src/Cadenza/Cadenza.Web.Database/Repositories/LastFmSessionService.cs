namespace Cadenza.Web.Database.Repositories;

internal class LastFmSessionService : ILastFmSessionService
{
    private readonly IUrl _url;
    private readonly IApiHttpHelper _apiHelper;
    private readonly DatabaseApiSettings _apiSettings;

    public LastFmSessionService(IUrl url, IApiHttpHelper http, IOptions<DatabaseApiSettings> apiSettings)
    {
        _url = url;
        _apiHelper = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task CreateSession(string token)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.LastFmCreateSession, ("token", token));
        await _apiHelper.Post(url);
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.LastFmAuthUrl, ("redirectUri", redirectUri));
        return await _apiHelper.Get(url);
    }

    public async Task<bool> DoesSessionExist()
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.LastFmHasSession);
        return await _apiHelper.Get<bool>(url);
    }
}
