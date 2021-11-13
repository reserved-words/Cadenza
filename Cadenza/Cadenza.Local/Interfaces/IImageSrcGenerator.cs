namespace Cadenza.Local;

public interface IImageSrcGenerator
{
    string GetImageSrc(AlbumFull album);
    string GetImageSrc(TrackFull track);
    string GetImageSrc(TrackSummary track);
}
