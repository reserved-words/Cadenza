using Cadenza.Common.Domain.Model;

namespace Cadenza.Local.API.Common.Controllers;

public interface ILibraryService
{
    Task<ArtworkImage> GetArtwork(string id);
    Task<string> GetPlayPath(string id);
}
