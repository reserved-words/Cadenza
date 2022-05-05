using Cadenza.Local.MusicFiles.Model;

namespace Cadenza.Local.MusicFiles.Interfaces;

internal interface IId3TagsService
{
    Id3Data GetId3Data(string filepath);

    void SaveId3Data(string filepath, Id3Data data);

    (byte[] Bytes, string Type) GetArtwork(string filepath);
}
