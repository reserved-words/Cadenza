namespace Cadenza.Local;

public class ImageSrcGenerator : IImageSrcGenerator
{
    private readonly IBase64Converter _base64Converter;
    private readonly IId3TagsService _id3TagsService;

    public ImageSrcGenerator(IBase64Converter base64Converter, IId3TagsService id3TagsService)
    {
        _base64Converter = base64Converter;
        _id3TagsService = id3TagsService;
    }

    public string GetImageSrc(AlbumFull album)
    {
        return GetImageSrc(album.AlbumTracks.First().Track);
    }

    public string GetImageSrc(FullTrack track)
    {
        return GetImageSrc(track);
    }

    private string GetImageSrc(Track track)
    {
        return GetImageSrc(track.Id);
    }

    public string GetImageSrc(string trackId)
    {
        var artwork = GetArtwork(trackId);
        return _base64Converter.ToImageSrc(artwork.Bytes);
    }

    public (byte[] Bytes, string Type) GetArtwork(string id)
    {
        var trackPath = _base64Converter.FromBase64(id);
        return _id3TagsService.GetArtwork(trackPath);
    }
}
