using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.MusicFiles.Interfaces;

namespace Cadenza.Local.MusicFiles;

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
