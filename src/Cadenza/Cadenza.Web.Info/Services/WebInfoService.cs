using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Results;
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

    public async Task<string> GetAlbumArtworkUrl(Album album)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AlbumArtworkUrl, ("artist", album.ArtistName), ("title", album.Title));
        var result = await _httpHelper.Get<AlbumArtworkResult>(url);
        return result.Url;
    }

    public async Task<string> GetArtistImageUrl(Artist artist)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.ArtistImageUrl, ("name", artist.Name));
        var result = await _httpHelper.Get<ArtistImageResult>(url);
        return result.Url;
    }
}
