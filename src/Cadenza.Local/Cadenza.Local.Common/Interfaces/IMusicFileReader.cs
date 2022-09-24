namespace Cadenza.Local.Common.Interfaces;

public interface IMusicFileArtworkService
{
    (byte[] Bytes, string Type) GetArtwork(string filepath);
}
