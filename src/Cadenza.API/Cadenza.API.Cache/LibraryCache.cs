namespace Cadenza.API.Cache;

internal class LibraryCache : ILibraryCache
{
    private readonly ICacheService _cache;

    public LibraryCache(ICacheService cache)
    {
        _cache = cache;
    }

    public IAlbumRepository AlbumCache => _cache;

    public IArtistRepository ArtistCache => _cache;

    public IPlayTrackRepository PlayTrackCache => _cache;

    public ISearchRepository SearchCache => _cache;

    public ITagRepository TagCache => _cache;

    public ITrackRepository TrackCache => _cache;

    public bool IsPopulated { get; private set; } = false;

    public async Task Populate(FullLibrary library)
    {
        await _cache.Populate(library);
        IsPopulated = true;
    }
}
