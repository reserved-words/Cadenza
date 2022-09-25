using Cadenza.Local.API.Files.Model;

namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3TagsService
{
    Id3Data GetId3Data(string filepath);

    void SaveId3Data(string filepath, Id3Data data);

    (byte[] Bytes, string Type) GetArtwork(string filepath);
}
