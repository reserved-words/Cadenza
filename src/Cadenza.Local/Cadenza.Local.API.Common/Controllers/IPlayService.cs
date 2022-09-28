namespace Cadenza.Local.API.Common.Controllers;

public interface ILibraryService
{
    Task<(byte[] Bytes, string Type)> GetArtwork(string id);
    Task<string> GetPlayPath(string id);
}
