namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IImages
{
    Task<AlbumArtwork> GetAlbumArtwork(int albumId);
    Task<ArtistImage> GetArtistImage(int artistId);
}
