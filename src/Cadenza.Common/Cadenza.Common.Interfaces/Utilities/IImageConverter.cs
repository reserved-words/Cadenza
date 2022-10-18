using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Utilities;

public interface IImageConverter
{
    ArtworkImage GetImageFromBase64Url(string base64Url);
    string GetBase64UrlFromImage(ArtworkImage artwork);
}
