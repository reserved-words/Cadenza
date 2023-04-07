using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary;

internal class ImageRepository : IImageRepository
{
    private readonly ILibraryReader _libraryReader;

    public ImageRepository(ILibraryReader libraryReader)
    {
        _libraryReader = libraryReader;
    }

    public async Task<string> GetAlbumArtwork(int albumId)
    {
        return await _libraryReader.GetAlbumArtwork(albumId);
    }

    public async Task<string> GetArtistImage(string nameId)
    {
        return await _libraryReader.GetArtistImage(nameId);
    }
}
