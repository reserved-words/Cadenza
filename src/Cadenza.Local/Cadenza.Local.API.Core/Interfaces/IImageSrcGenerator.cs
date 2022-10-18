namespace Cadenza.Local.API.Core.Interfaces;

internal interface IImageSrcGenerator
{
    ArtworkImage GetArtwork(string id);
}
