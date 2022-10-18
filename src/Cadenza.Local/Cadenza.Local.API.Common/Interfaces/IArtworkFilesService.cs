using Cadenza.Common.Domain.Model;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IArtworkFilesService
{
    ArtworkImage GetArtwork(string filepath);
}
