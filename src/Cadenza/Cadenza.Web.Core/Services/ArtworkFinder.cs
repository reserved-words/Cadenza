namespace Cadenza.Web.Core.Services;

internal class ArtworkFinder : IArtworkFinder
{
    private const string ArtworkSearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";

    private readonly IHttpHelper _httpHelper;
    private readonly IImageConverter _imageConverter;
    private readonly IWebInfoService _webInfoService;

    public ArtworkFinder(IHttpHelper httpHelper, IImageConverter imageConverter, IWebInfoService webInfoService)
    {
        _httpHelper = httpHelper;
        _imageConverter = imageConverter;
        _webInfoService = webInfoService;
    }

    public async Task<string> GetBase64ArtworkSource(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
        {
            throw new Exception("URL is invalid");
        }

        try
        {
            var response = await _httpHelper.Get(uri.ToString());

            if (!response.IsSuccessStatusCode)
            {
                // Log exact error
                throw new Exception("Access was not allowed");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var mimeType = response.Content.Headers.ContentType.MediaType;

            if (!mimeType.StartsWith("image/"))
            {
                throw new Exception("Not an image URL");
            }

            var image = new ArtworkImage(bytes, mimeType);
            return _imageConverter.GetBase64UrlFromImage(image);

        }
        catch (Exception ex)
        {
            // Log exact error
            throw new Exception("Fetching image failed");
        }
    }

    public string GetSearchUrl(AlbumInfo model)
    {
        var artist = HttpUtility.UrlEncode(model.ArtistName);
        var title = HttpUtility.UrlEncode(model.Title);
        return string.Format(ArtworkSearchUrl, artist, title);
    }

    public async Task<string> GetUrl(AlbumInfo model)
    {
        return await _webInfoService.GetAlbumArtworkUrl(model);
    }
}
