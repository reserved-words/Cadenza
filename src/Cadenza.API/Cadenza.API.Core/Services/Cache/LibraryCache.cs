namespace Cadenza.API.Core.Services.Cache;

internal class LibraryCache : ILibraryCache
{
    private readonly ICache _cache;
    private readonly IPlayTrackCache _playTrackCache;

    public LibraryCache(ICache cache, IPlayTrackCache playTrackCache)
    {
        _cache = cache;
        _playTrackCache = playTrackCache;
    }

    public IAlbumRepository AlbumCache => _cache;

    public IArtistRepository ArtistCache => _cache;

    public IPlayTrackRepository PlayTrackCache => _playTrackCache;

    public ISearchRepository SearchCache => _cache;

    public ITagRepository TagCache => _cache;

    public ITrackRepository TrackCache => _cache;

    public bool IsPopulated { get; private set; } = false;

    public async Task Populate(FullLibrary library)
    {
        await _cache.Populate(library);
        await _playTrackCache.Populate(library);
        IsPopulated = true;
    }
}
