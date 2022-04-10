namespace Cadenza.Local.Common.Interfaces;

public interface IImageSrcGenerator
{
    (byte[] Bytes, string Type) GetArtwork(string id);
}
