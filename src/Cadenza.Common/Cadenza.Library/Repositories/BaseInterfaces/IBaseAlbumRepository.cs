namespace Cadenza.Library;

public interface IBaseAlbumRepository : IAlbumRepository
{
    Task Populate();
}
