using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Api.Settings;

namespace Cadenza.Web.Api.Services;

internal class LastFmApi : ILastFmApi
{
    private readonly IUrl _url;
    private readonly IApiHttpHelper _apiHelper;
    private readonly ApiSettings _apiSettings;

    public LastFmApi(IUrl url, IApiHttpHelper http, IOptions<ApiSettings> apiSettings)
    {
        _url = url;
        _apiHelper = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task CreateSession(string token)
    {
        var url = _url.Build(_apiSettings.Endpoints.LastFmCreateSession, ("token", token));
        await _apiHelper.Post(url);
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var url = _url.Build(_apiSettings.Endpoints.LastFmAuthUrl, ("redirectUri", redirectUri));
        return await _apiHelper.Get(url);
    }

    public async Task<bool> DoesSessionExist()
    {
        var url = _url.Build(_apiSettings.Endpoints.LastFmHasSession);
        return await _apiHelper.Get<bool>(url);
    }
}
