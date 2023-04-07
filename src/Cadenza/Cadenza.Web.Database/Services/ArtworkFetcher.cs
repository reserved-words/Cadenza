using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.Web.Database.Services;

internal class ArtworkFetcher : IArtworkFetcher
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly DatabaseApiSettings _settings;

    public ArtworkFetcher(IOptions<DatabaseApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GetArtistImageSrc(ArtistInfo artist)
    {
        if (artist?.ImageBase64 != null)
            return artist.ImageBase64;

        return artist?.Id != null
            ? GetUrl(_settings.Endpoints.ArtistImage, artist.Id)
            : ArtworkPlaceholderUrl;
    }

    public string GetAlbumArtworkSrc(Album album)
    {
        if (album?.ArtworkBase64 != null)
            return album.ArtworkBase64;

        return album?.Id != null
            ? GetUrl(_settings.Endpoints.AlbumArtwork, album.Id)
            : ArtworkPlaceholderUrl;
    }
    
    private string GetUrl(string endpoint, object id)
    {
        return $"{_settings.BaseUrl}{string.Format(endpoint, id)}";
    }
}
