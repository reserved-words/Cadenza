using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Results;
using Cadenza.Common.Interfaces.Utilities;
using Cadenza.Web.Info.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Info.Services;

internal class WebInfoService : IWebInfoService
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly InfoApiSettings _apiSettings;

    public WebInfoService(IUrl url, IHttpHelper http, IOptions<InfoApiSettings> apiSettings)
    {
        _url = url;
        _http = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task<string> GetAlbumArtworkUrl(Album album)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AlbumArtworkUrl, ("artist", album.ArtistName), ("title", album.Title));
        var response = await _http.Get(url);
        var result = await response.Content.ReadFromJsonAsync<AlbumArtworkResult>();
        return result.Url;
    }

    public async Task<string> GetArtistImageUrl(Artist artist)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.ArtistImageUrl, ("name", artist.Name));
        var response = await _http.Get(url);
        var result = await response.Content.ReadFromJsonAsync<ArtistImageResult>();
        return result.Url;
    }
}
