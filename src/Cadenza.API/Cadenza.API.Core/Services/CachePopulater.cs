using Cadenza.API.Common.Repositories;

namespace Cadenza.API.Core.Services;

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
        if (_cache.IsPopulated && onlyIfEmpty)
            return;

        var library = await _repository.Get();
        await _cache.Populate(library);
    }
}
