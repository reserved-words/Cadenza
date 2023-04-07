namespace Cadenza.Web.Database.Services;

internal class ArtworkFetcher : IArtworkFetcher
{
    private const string ArtworkPlaceholderUrl = "images/artwork-placeholder.png";

    private readonly DatabaseApiSettings _settings;

    public ArtworkFetcher(IOptions<DatabaseApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string> GetArtistImageUrl(ArtistInfo artist, string trackId = null)
    {
        if (artist != null && artist.ImageUrl == null)
        {
            artist.ImageUrl = GetUrl(_settings.Endpoints.ArtistImage, artist.Id);
        }

        return artist?.ImageUrl ?? ArtworkPlaceholderUrl;
    }

    public async Task<string> GetArtworkUrl(Album album, string trackId = null)
    {
        if (album != null && album.ArtworkUrl == null)
        {
            album.ArtworkUrl = GetUrl(_settings.Endpoints.AlbumArtwork, album.Id);
        }

        return album?.ArtworkUrl ?? ArtworkPlaceholderUrl;
    }
    
    private string GetUrl(string endpoint, object id)
    {
        return $"{_settings.BaseUrl}{string.Format(endpoint, id)}";
    }
}
