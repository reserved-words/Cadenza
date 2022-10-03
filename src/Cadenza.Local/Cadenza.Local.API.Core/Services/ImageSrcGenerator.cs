namespace Cadenza.Local.API.Core.Services;

internal class ImageSrcGenerator : IImageSrcGenerator
{
    private readonly IBase64Converter _base64Converter;
    private readonly IArtworkFilesService _musicFileLibrary;

    public ImageSrcGenerator(IBase64Converter base64Converter, Common.Interfaces.IArtworkFilesService musicFileLibrary)
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
