namespace Cadenza.API.Interfaces.Library;

public interface IArtistRepository
{
    Task<List<ArtistDTO>> GetArtistsByGenre(string id);
    Task<List<ArtistDTO>> GetArtistsByGrouping(int id);
}
