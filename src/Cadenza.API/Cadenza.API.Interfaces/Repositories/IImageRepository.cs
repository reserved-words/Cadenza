namespace Cadenza.API.Interfaces.Repositories;

public interface IImageRepository
{
    Task<ArtworkImage> GetArtistImage(int artistId);
    Task<ArtworkImage> GetAlbumArtwork(int albumId);
}
