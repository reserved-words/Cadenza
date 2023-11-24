using Cadenza.API.Interfaces.Services;

namespace Cadenza.API.Cache;

internal class LibraryCache : ILibraryCache
{
    private readonly ICacheService _cache;

    public LibraryCache(ICacheService cache)
    {
        _cache = cache;
    }

    public IArtistRepository Artists => _cache;

    public ITrackRepository Tracks => _cache;

    public bool IsPopulated { get; private set; } = false;

    public async Task Populate(FullLibraryDTO library)
    {
        await _cache.Populate(library);
        IsPopulated = true;
    }
}
