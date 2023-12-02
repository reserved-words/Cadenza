namespace Cadenza.Web.Api.Services;

internal class ArtworkApi : IArtworkApi
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly ApiSettings _settings;

    public ArtworkApi(IOptions<ApiSettings> settings)
    {
        _settings = settings.Value;
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

    private string GetUrl(string endpoint, int id)
    {
        return $"{_settings.BaseUrl}{string.Format(endpoint, id)}";
    }
}
