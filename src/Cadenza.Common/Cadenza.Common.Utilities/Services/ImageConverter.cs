using Cadenza.Common.Utilities.Interfaces;
using System.Text.RegularExpressions;

namespace Cadenza.Common.Utilities.Services;

internal class ImageConverter : IImageConverter
{
    private const string Base64UrlFormat = "data:{0};base64, {1}";
    private const string RegexFormat = @"^data\:{0}\;base64\,\s{1}$";

    private const string MimeTypePattern = @"([a-zA-Z0-9\/]*)";
    private const string Base64StringPattern = @"(.*)";

    public string GetBase64UrlFromImage(ArtworkImage artwork)
    {
        if (artwork == null)
            return null;

        var base64String = Convert.ToBase64String(artwork.Bytes);
        return string.Format(Base64UrlFormat, artwork.MimeType, base64String);
    }

    public ArtworkImage GetImageFromBase64Url(string base64Url)
    {
        if (base64Url == null)
            return null;

        var pattern = string.Format(RegexFormat, MimeTypePattern, Base64StringPattern);

        var regex = new Regex(pattern);
        var match = regex.Match(base64Url);

        var mimeType = match.Groups[1].Value;
        var base64String = match.Groups[2].Value;

        var bytes = Convert.FromBase64String(base64String);

        return new ArtworkImage(bytes, mimeType);
    }
}