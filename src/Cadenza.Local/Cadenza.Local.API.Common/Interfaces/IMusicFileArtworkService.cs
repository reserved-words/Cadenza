namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFileArtworkService
{
    (byte[] Bytes, string Type) GetArtwork(string filepath);
}
