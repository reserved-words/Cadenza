namespace Cadenza.Web.Core.Services;

internal class ImageFinder : IImageFinder
{
    private const string AlbumSearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";
    private const string ArtistSearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";

    private readonly IHttpHelper _httpHelper;
    private readonly IImageConverter _imageConverter;
    private readonly IArtworkApi _artworkApi;

    public ImageFinder(IHttpHelper httpHelper, IImageConverter imageConverter, IArtworkApi artworkApi)
    {
        _httpHelper = httpHelper;
        _imageConverter = imageConverter;
        _artworkApi = artworkApi;
    }

    public async Task<string> GetBase64ArtworkSource(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
        {
            throw new Exception("URL is invalid");
        }

        try
        {
            var image = await _httpHelper.GetImage(url);

            return _imageConverter.GetBase64UrlFromImage(image);

        }
        catch
        {
            // Log exact error
            throw new Exception("Fetching image failed");
        }
    }

    public string GetAlbumArtworkSearchUrl(string artist, string title)
    {
        var searchArtist = HttpUtility.UrlEncode(artist);
        var searchTitle = HttpUtility.UrlEncode(title);
        return string.Format(AlbumSearchUrl, searchArtist, searchTitle);
    }

    public string GetArtistImageSearchUrl(string name, SearchSource source)
    {
        var searchName = HttpUtility.UrlEncode(name);
        return string.Format(ArtistSearchUrl, searchName, source.GetDisplayName());
    }

    public async Task<string> GetAlbumArtworkUrl(string artist, string title)
    {
        return await _artworkApi.FindAlbumArtworkUrl(artist, title);
    }
}
