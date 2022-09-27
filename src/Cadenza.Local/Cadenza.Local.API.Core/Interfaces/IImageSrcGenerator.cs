namespace Cadenza.Local.API.Core.Interfaces;

internal interface IImageSrcGenerator
{
    (byte[] Bytes, string Type) GetArtwork(string id);
}
