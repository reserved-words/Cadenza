namespace Cadenza.API.Core.Services.Cache;

internal class LibraryCache : ILibraryCache
{
    private readonly ICache _cache;

    public LibraryCache(ICache cache)
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
