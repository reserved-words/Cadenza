namespace Cadenza.Local.API.Common.Controllers;

public interface ILibraryService
{
    Task<string> GetPlayPath(string id);
}
