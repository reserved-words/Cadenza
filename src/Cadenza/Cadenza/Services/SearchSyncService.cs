using Cadenza.Library;

namespace Cadenza;

public class SearchSyncService : ISearchSyncService
{
    private const int ItemFetchLimit = 500;

    private readonly IEnumerable<ISourceSearchRepository> _repositories;

    private readonly SearchRepositoryCache _cache;

    public SearchSyncService(IEnumerable<ISourceSearchRepository> repositories, SearchRepositoryCache cache)
    {
        _repositories = repositories;
        _cache = cache;
    }

    public async Task PopulateSearchItems()
    {
        _cache.StartUpdate();

        _cache.Clear();

        var reps = string.Join(", ", _repositories.Select(r => r.Source));

        foreach (var repository in _repositories)
        {
            await FetchArtists(repository);
            await FetchAlbums(repository);
            await FetchTracks(repository);
        }

        _cache.FinishUpdate();
    }

    private async Task FetchTracks(ISourceSearchRepository repository)
    {
        var response = await repository.GetSearchTracks(1, ItemFetchLimit);
        _cache.AddTracks(repository.Source, response.Items);

        while (response.Page < response.TotalPages)
        {
            response = await repository.GetSearchTracks(response.Page + 1, ItemFetchLimit);
            _cache.AddTracks(repository.Source, response.Items);
        }
    }

    private async Task FetchAlbums(ISourceSearchRepository repository)
    {
        var response = await repository.GetSearchAlbums(1, ItemFetchLimit);
        _cache.AddAlbums(repository.Source, response.Items);

        while (response.Page < response.TotalPages)
        {
            response = await repository.GetSearchAlbums(response.Page + 1, ItemFetchLimit);
            _cache.AddAlbums(repository.Source, response.Items);
        }
    }

    private async Task FetchArtists(ISourceSearchRepository repository)
    {
        var response = await repository.GetSearchArtists(1, ItemFetchLimit);
        _cache.AddArtists(repository.Source, response.Items);

        while (response.Page < response.TotalPages)
        {
            response = await repository.GetSearchArtists(response.Page + 1, ItemFetchLimit);
            _cache.AddArtists(repository.Source, response.Items);
        }
    }
}