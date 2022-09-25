namespace Cadenza.Local.Common.Interfaces;

internal interface IImageSrcGenerator
{
    (byte[] Bytes, string Type) GetArtwork(string id);
}
