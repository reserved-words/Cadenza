using Cadenza.API.Interfaces.Services;

namespace Cadenza.API.Services;

internal class CachePopulater : ICachePopulater
{
    private readonly ILibraryCache _cache;
    private readonly ILibraryRepository _repository;

    public CachePopulater(ILibraryCache cache, ILibraryRepository repository)
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
