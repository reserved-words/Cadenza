using Cadenza.Local.Common.Model.Id3;

namespace Cadenza.Local.Common.Interfaces;

public interface IId3TagsService
{
    Id3Data GetId3Data(string filepath);

    void SaveId3Data(string filepath, Id3Data data);

    (byte[] Bytes, string Type) GetArtwork(string filepath);
}
