namespace Cadenza.API.Cache;

internal class LibraryCache : ILibraryCache
{
    private readonly ICacheService _cache;

    public LibraryCache(ICacheService cache)
    {
        _cache = cache;
    }

    public IAlbumRepository Albums => _cache;

    public IArtistRepository Artists => _cache;

    public IPlayTrackRepository PlayTracks => _cache;

    public ISearchRepository Search => _cache;

    public ITagRepository Tags => _cache;

    public ITrackRepository Tracks => _cache;

    public bool IsPopulated { get; private set; } = false;

    public async Task Populate(FullLibraryDTO library)
    {
        await _cache.Populate(library);
        IsPopulated = true;
    }
}
