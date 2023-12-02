namespace Cadenza.Web.Database.Services;

internal class WebInfoService : IWebInfoService
{
    private readonly IUrl _url;
    private readonly IApiHttpHelper _httpHelper;
    private readonly DatabaseApiSettings _apiSettings;

    public WebInfoService(IUrl url, IApiHttpHelper httpHelper, IOptions<DatabaseApiSettings> apiSettings)
    {
        _url = url;
        _httpHelper = httpHelper;
        _apiSettings = apiSettings.Value;
    }

    public async Task<string> GetAlbumArtworkUrl(string artist, string title)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AlbumArtworkUrl, ("artist", artist), ("title", title));
        var result = await _httpHelper.Get<AlbumArtworkDTO>(url);
        return result.Url;
    }
}
