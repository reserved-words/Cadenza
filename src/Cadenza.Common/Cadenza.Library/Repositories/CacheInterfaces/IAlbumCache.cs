namespace Cadenza.Library;

public interface IAlbumCache : IAlbumRepository
{
    Task Populate(FullLibrary library);
    Task UpdateAlbum(AlbumUpdate update);
}
