namespace Cadenza.Local.API.Core.Interfaces;

internal interface IImageSrcGenerator
{
    ArtworkImage GetArtistImage(string id);
    ArtworkImage GetArtwork(string id);
}
