namespace Cadenza.Library;

public interface IBasePlaylistRepository : IPlaylistRepository
{
    Task Populate();
}
