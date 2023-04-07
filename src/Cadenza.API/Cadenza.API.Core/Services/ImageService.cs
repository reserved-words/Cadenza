using Cadenza.Common.Interfaces.Utilities;

namespace Cadenza.API.Core.Services;

internal class ImageService : IImageService
{
    private readonly IImageConverter _converter;
    private readonly IImageRepository _repository;

    public ImageService(IImageRepository repository, IImageConverter converter)
    {
        _repository = repository;
        _converter = converter;
    }

    public async Task<ArtworkImage> GetArtistImage(string nameId)
    {
        var imageBase64 = await _repository.GetArtistImage(nameId);

        if (imageBase64 == null)
            return GetDefaultImage();

        return _converter.GetImageFromBase64Url(imageBase64);
    }

    public async Task<ArtworkImage> GetAlbumArtwork(int albumId)
    {
        var imageBase64 = await _repository.GetAlbumArtwork(albumId);

        if (imageBase64 == null)
            return GetDefaultImage();

        return _converter.GetImageFromBase64Url(imageBase64);
    }

    private ArtworkImage GetDefaultImage()
    {
        var bytes = File.ReadAllBytes("Images/default.png");
        return new ArtworkImage(bytes, "image/png");
    }
}