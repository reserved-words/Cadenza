namespace Cadenza.Local.API.Files;

internal class ArtworkService : IArtworkFilesService
{
    private readonly IId3TagsService _id3Service;

    public ArtworkService(IId3TagsService id3Service)
    {
        _id3Service = id3Service;
    }

    public (byte[] Bytes, string Type) GetArtwork(string filepath)
    {
        return _id3Service.GetArtwork(filepath);
    }
}
