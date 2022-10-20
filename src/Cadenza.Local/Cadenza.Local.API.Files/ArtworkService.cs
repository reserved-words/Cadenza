using Cadenza.Common.Domain.Model;

namespace Cadenza.Local.API.Files;

internal class ArtworkService : IArtworkFilesService
{
    private readonly IId3TagsService _id3Service;

    public ArtworkService(IId3TagsService id3Service)
    {
        _id3Service = id3Service;
    }

    public ArtworkImage GetArtistImage(string filepath)
    {
        return _id3Service.GetArtistImage(filepath);
    }

    public ArtworkImage GetArtwork(string filepath)
    {
        return _id3Service.GetAlbumArtwork(filepath);
    }
}
