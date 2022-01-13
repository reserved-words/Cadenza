namespace Cadenza.Local;

public interface IImageSrcGenerator
{
    (byte[] Bytes, string Type) GetArtwork(string id);
}
