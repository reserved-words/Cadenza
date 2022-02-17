namespace Cadenza.Library;

public class BaseAlbumRepository : IBaseAlbumRepository
{
    private readonly ILibrary _library;

    private Dictionary<string, AlbumInfo> _albums;

    public BaseAlbumRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<AlbumInfo> GetAlbum(string id)
    {
        return _albums[id];
    }

    public async Task Populate()
    {
        var library = await _library.Get();

        _albums = library.Albums.ToDictionary(a => a.Id, a => a);
    }
}
