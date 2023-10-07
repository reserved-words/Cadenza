using Cadenza.Common.DTO;
using Cadenza.Web.Info.Interfaces;
using Cadenza.Web.Info.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Info.Services;

internal class WebInfoService : IWebInfoService
{
    private readonly IUrl _url;
    private readonly IWebInfoHttpHelper _httpHelper;
    private readonly InfoApiSettings _apiSettings;

    public WebInfoService(IUrl url, IWebInfoHttpHelper httpHelper, IOptions<InfoApiSettings> apiSettings)
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

    public async Task<string> GetArtistImageUrl(string name)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.ArtistImageUrl, ("name", name));
        var result = await _httpHelper.Get<ArtistImageDTO>(url);
        return result.Url;
    }
}
