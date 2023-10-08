using Cadenza.Common.Model;

namespace Cadenza.Common.Utilities.Interfaces;

public interface IImageConverter
{
    ArtworkImage GetImageFromBase64Url(string base64Url);
    string GetBase64UrlFromImage(ArtworkImage artwork);
}
