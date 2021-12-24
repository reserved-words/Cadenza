namespace Cadenza.Local;

public interface IImageSrcGenerator
{
    string GetImageSrc(AlbumFull album);
    string GetImageSrc(FullTrack track);
    //string GetImageSrc(TrackSummary track);
    string GetImageSrc(string trackId);
    (byte[] Bytes, string Type) GetArtwork(string id);
}
