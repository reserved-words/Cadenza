namespace Cadenza.Local.API;

public interface IArtworkService
{
    Task<(byte[] Bytes, string Type)> GetArtwork(string id);
}
