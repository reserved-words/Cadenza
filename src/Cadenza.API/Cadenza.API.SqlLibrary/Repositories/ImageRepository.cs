namespace Cadenza.Database.SqlLibrary.Repositories;

internal class ImageRepository : IImageRepository
{
    private readonly IImages _images;

    public ImageRepository(IImages images)
    {
        _images = images;
    }

    public async Task<ArtworkImage> GetAlbumArtwork(int albumId)
    {
        var data = await _images.GetAlbumArtwork(albumId);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }

    public async Task<ArtworkImage> GetArtistImage(int artistId)
    {
        var data = await _images.GetArtistImage(artistId);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }
}
