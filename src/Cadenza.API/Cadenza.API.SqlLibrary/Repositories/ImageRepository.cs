namespace Cadenza.Database.SqlLibrary.Repositories;

internal class ImageRepository : IImageRepository
{
    private readonly ILibrary _library;

    public ImageRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<ArtworkImage> GetAlbumArtwork(int albumId)
    {
        var data = await _library.GetAlbumArtwork(albumId);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }

    public async Task<ArtworkImage> GetArtistImage(int artistId)
    {
        var data = await _library.GetArtistImage(artistId);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }
}
