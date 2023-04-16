using Cadenza.API.Interfaces;

namespace Cadenza.API.Core;

internal class CachePopulater : ICachePopulater
{
    private readonly ILibraryCache _cache;
    private readonly IMusicRepository _repository;

    public CachePopulater(ILibraryCache cache, IMusicRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }

    public async Task Populate(bool onlyIfEmpty)
    {
        if (onlyIfEmpty && _cache.IsPopulated)
            return;

        var library = await _repository.Get();
        await _cache.Populate(library);
    }
}
