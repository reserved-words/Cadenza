namespace Cadenza.Local.API.Common.Controllers;

public interface IArtworkService
{
    Task<(byte[] Bytes, string Type)> GetArtwork(string id);
}
