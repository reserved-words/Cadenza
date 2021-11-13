namespace Cadenza.Local;

public interface IId3TagsService
{
    Id3Data GetId3Data(string filepath);

    void SaveId3Data(string filepath, Id3Data data);

    byte[] GetArtworkBytes(string filepath);

}
