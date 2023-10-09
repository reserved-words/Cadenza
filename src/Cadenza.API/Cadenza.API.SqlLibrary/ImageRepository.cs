namespace Cadenza.API.SqlLibrary;

internal class ImageRepository : IImageRepository
{
    private readonly ILibraryReader _libraryReader;

    public ImageRepository(ILibraryReader libraryReader)
    {
        _libraryReader = libraryReader;
    }

    public async Task<ArtworkImage> GetAlbumArtwork(int albumId)
    {
        return await _libraryReader.GetAlbumArtwork(albumId);
    }

    public async Task<ArtworkImage> GetArtistImage(int artistId)
    {
        return await _libraryReader.GetArtistImage(artistId);
    }
}
