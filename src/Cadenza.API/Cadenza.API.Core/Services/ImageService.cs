namespace Cadenza.API.Core.Services;

internal class ImageService : IImageService
{
    private readonly IImageRepository _repository;

    public ImageService(IImageRepository repository)
    {
        _repository = repository;
    }

    public async Task<ArtworkImage> GetArtistImage(int id)
    {
        var image = await _repository.GetArtistImage(id);
        return image ?? GetDefaultImage();
    }

    public async Task<ArtworkImage> GetAlbumArtwork(int albumId)
    {
        var image = await _repository.GetAlbumArtwork(albumId);
        return image ?? GetDefaultImage();
    }

    private ArtworkImage GetDefaultImage()
    {
        var bytes = File.ReadAllBytes("Images/default.png");
        return new ArtworkImage(bytes, "image/png");
    }
}