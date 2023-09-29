using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Database.Services;

internal class ArtworkFetcher : IArtworkFetcher
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly DatabaseApiSettings _settings;

    private readonly Dictionary<int, string> _updateAlbumArtwork = new();
    private readonly Dictionary<int, string> _updatedArtistImages = new ();

    public ArtworkFetcher(IOptions<DatabaseApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GetArtistImageSrc(ArtistDetails artist)
    {
        if (artist == null || artist.Id == 0)
            return ArtworkPlaceholderUrl;

        if (artist.ImageBase64 != null)
        {
            _updatedArtistImages.Remove(artist.Id);
            _updatedArtistImages.Add(artist.Id, artist.ImageBase64);
        }

        if (_updatedArtistImages.TryGetValue(artist.Id, out var imageSrc))
            return imageSrc;

        return GetUrl(_settings.Endpoints.ArtistImage, artist.Id);
    }

    public string GetAlbumArtworkSrc(Album album)
    {
        if (album == null || album.Id == 0)
            return ArtworkPlaceholderUrl;

        if (album.ArtworkBase64 != null)
        {
            _updateAlbumArtwork.Remove(album.Id);
            _updateAlbumArtwork.Add(album.Id, album.ArtworkBase64);
        }

        if (_updateAlbumArtwork.TryGetValue(album.Id, out var imageSrc))
            return imageSrc;

        return GetUrl(_settings.Endpoints.AlbumArtwork, album.Id);
    }

    private string GetUrl(string endpoint, int id)
    {
        return $"{_settings.BaseUrl}{string.Format(endpoint, id)}";
    }
}
