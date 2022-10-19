using Cadenza.Common.Domain.Model;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IArtworkFilesService
{
    ArtworkImage GetArtistImage(string filepath);
    ArtworkImage GetArtwork(string filepath);
}
