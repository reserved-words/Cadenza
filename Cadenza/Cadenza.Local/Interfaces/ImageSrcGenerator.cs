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

    public string GetImageSrc(TrackFull track)
    {
        return GetImageSrc(track.Track);
    }

    public string GetImageSrc(TrackSummary track)
    {
        return GetImageSrc(track.Track);
    }

    private string GetImageSrc(Track track)
    {
        var trackPath = _base64Converter.FromBase64(track.Id);
        var artworkBytes = _id3TagsService.GetArtworkBytes(trackPath);
        return _base64Converter.ToImageSrc(artworkBytes);
    }
}
