namespace Cadenza.Library;

public interface IBaseArtistRepository : IArtistRepository
{
    Task Populate();
}
