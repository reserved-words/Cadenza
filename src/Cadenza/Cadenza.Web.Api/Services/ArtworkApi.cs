namespace Cadenza.Web.Api.Services;

internal class ArtworkApi : IArtworkApi
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly ApiSettings _settings;
    private readonly IApiHttpHelper _httpHelper;
    private readonly IUrl _url;

    public ArtworkApi(IOptions<ApiSettings> settings, IUrl url, IApiHttpHelper httpHelper)
    {
        _settings = settings.Value;
        _url = url;
        _httpHelper = httpHelper;
    }

    public string GetArtistImageUrl(int? artistId)
    {
        if (artistId == null || artistId == 0)
            return ArtworkPlaceholderUrl;

        return GetUrl(_settings.Endpoints.ArtistImage, artistId.Value);
    }

    public string GetAlbumArtworkUrl(int? albumId)
    {
        if (albumId == null || albumId == 0)
            return ArtworkPlaceholderUrl;

        return GetUrl(_settings.Endpoints.AlbumArtwork, albumId.Value);
    }

    public async Task<string> FindAlbumArtworkUrl(string artist, string title)
    {
        var url = _url.Build(_settings.BaseUrl, _settings.Endpoints.AlbumArtworkUrl, ("artist", artist), ("title", title));
        var result = await _httpHelper.Get<AlbumArtworkDTO>(url);
        return result.Url;
    }

    private string GetUrl(string endpoint, int id)
    {
        return $"{_settings.BaseUrl}{string.Format(endpoint, id)}";
    }
}
