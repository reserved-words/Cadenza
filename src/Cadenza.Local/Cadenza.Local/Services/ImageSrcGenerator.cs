using Cadenza.Local.Common.Interfaces;

namespace Cadenza.Local.Services;

public class ImageSrcGenerator : IImageSrcGenerator
{
    private readonly IBase64Converter _base64Converter;
    private readonly IId3TagsService _id3TagsService;

    public ImageSrcGenerator(IBase64Converter base64Converter, IId3TagsService id3TagsService)
    {
        _base64Converter = base64Converter;
        _id3TagsService = id3TagsService;
    }

    public (byte[] Bytes, string Type) GetArtwork(string id)
    {
        var trackPath = _base64Converter.FromBase64(id);
        return _id3TagsService.GetArtwork(trackPath);
    }
}
