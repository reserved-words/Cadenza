namespace Cadenza.API.Interfaces.Controllers;

public interface IImageService
{
    Task<ArtworkImage> GetArtistImage(int id);
    Task<ArtworkImage> GetAlbumArtwork(int id);
}
