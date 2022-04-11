using Cadenza.Local.Common.Interfaces;

namespace Cadenza.Local.Services;

public class ImageSrcGenerator : IImageSrcGenerator
{
    private readonly IBase64Converter _base64Converter;
    private readonly IMusicFileLibrary _musicFileLibrary;

    public ImageSrcGenerator(IBase64Converter base64Converter, IMusicFileLibrary musicFileLibrary)
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
