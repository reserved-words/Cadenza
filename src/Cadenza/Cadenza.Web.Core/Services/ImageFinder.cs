using Cadenza.Common.Domain.Model.Library;
using Cadenza.Common.Utilities.Interfaces;

namespace Cadenza.Web.Core.Services;

internal class ImageFinder : IImageFinder
{
    private const string AlbumSearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";
    private const string ArtistSearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";

    private readonly IHttpHelper _httpHelper;
    private readonly IImageConverter _imageConverter;
    private readonly IWebInfoService _webInfoService;

    public ImageFinder(IHttpHelper httpHelper, IImageConverter imageConverter, IWebInfoService webInfoService)
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
            var image = await _httpHelper.GetImage(url);

            return _imageConverter.GetBase64UrlFromImage(image);

        }
        catch
        {
            // Log exact error
            throw new Exception("Fetching image failed");
        }
    }

    public string GetSearchUrl(AlbumDetails model)
    {
        var artist = HttpUtility.UrlEncode(model.ArtistName);
        var title = HttpUtility.UrlEncode(model.Title);
        return string.Format(AlbumSearchUrl, artist, title);
    }

    public string GetSearchUrl(ArtistDetails model, SearchSource source)
    {
        var name = HttpUtility.UrlEncode(model.Name);
        return string.Format(ArtistSearchUrl, name, source.GetDisplayName());
    }

    public async Task<string> GetUrl(AlbumDetails model)
    {
        return await _webInfoService.GetAlbumArtworkUrl(model);
    }
}
