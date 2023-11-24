using Cadenza.Common.Model;

namespace Cadenza.Database.Interfaces;

public interface IImageRepository
{
    Task<ArtworkImage> GetArtistImage(int artistId);
    Task<ArtworkImage> GetAlbumArtwork(int albumId);
}
