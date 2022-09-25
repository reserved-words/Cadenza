using Cadenza.Local.API.Common.Interfaces;
using Cadenza.Local.API.Files.Interfaces;

namespace Cadenza.Local.API.Files;

internal class MusicFileArtworkService : IMusicFileArtworkService
{
    private readonly IId3TagsService _id3Service;

    public MusicFileArtworkService(IId3TagsService id3Service)
    {
        _id3Service = id3Service;
    }

    public (byte[] Bytes, string Type) GetArtwork(string filepath)
    {
        return _id3Service.GetArtwork(filepath);
    }
}
