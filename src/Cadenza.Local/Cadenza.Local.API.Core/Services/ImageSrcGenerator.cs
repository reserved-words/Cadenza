using Cadenza.Local.API.Common.Interfaces;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Local.API.Core.Services;

internal class ImageSrcGenerator : IImageSrcGenerator
{
    private readonly IBase64Converter _base64Converter;
    private readonly IMusicFileArtworkService _musicFileLibrary;

    public ImageSrcGenerator(IBase64Converter base64Converter, IMusicFileArtworkService musicFileLibrary)
    {
        _base64Converter = base64Converter;
        _musicFileLibrary = musicFileLibrary;
    }

    public (byte[] Bytes, string Type) GetArtwork(string id)
    {
        var filepath = _base64Converter.FromBase64(id);
        return _musicFileLibrary.GetArtwork(filepath);
    }
}
