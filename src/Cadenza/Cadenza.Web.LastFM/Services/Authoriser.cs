namespace Cadenza.Web.LastFM.Services;

internal class Authoriser : IAuthoriser
{
    private readonly IUrl _url;
    private readonly ILastFmHttpHelper _http;
    private readonly LastFmApiSettings _apiSettings;

    public Authoriser(IUrl url, ILastFmHttpHelper http, IOptions<LastFmApiSettings> apiEndpoints)
    {
        _url = url;
        _http = http;
        _apiSettings = apiEndpoints.Value;
    }

    public async Task<string> CreateSession(string token)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.CreateSession, ("token", token));
        return await _http.Get(url);
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AuthUrl, ("redirectUri", redirectUri));
        return await _http.Get(url);
    }
}
