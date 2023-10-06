namespace Cadenza.Web.Database.Services;

internal class ArtworkFetcher : IArtworkFetcher
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly DatabaseApiSettings _settings;

    public ArtworkFetcher(IOptions<DatabaseApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GetArtistImageSrc(int? artistId)
    {
        if (artistId == null || artistId == 0)
            return ArtworkPlaceholderUrl;

        return GetUrl(_settings.Endpoints.ArtistImage, artistId.Value);
    }

    public string GetAlbumArtworkSrc(int? albumId)
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
