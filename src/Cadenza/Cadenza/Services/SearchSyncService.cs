using Cadenza.Library;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Core;

namespace Cadenza.Services;

public class SearchSyncService : ISearchSyncService
{
    private readonly ISearchRepository _repository;
    private readonly SearchRepositoryCache _cache;

    public SearchSyncService(ISearchRepository repository, SearchRepositoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task PopulateSearchItems()
    {
        _cache.StartUpdate();

        _cache.Clear();

        await FetchArtists();
        await FetchAlbums();
        await FetchTracks();
        await FetchGenres();
        await FetchGroupings();

        _cache.FinishUpdate();
    }

    private async Task FetchTracks()
    {
        var response = await _repository.GetSearchTracks();
        _cache.AddTracks(response);
    }

    private async Task FetchAlbums()
    {
        var response = await _repository.GetSearchAlbums();
        _cache.AddAlbums(response);
    }

    private async Task FetchArtists()
    {
        var response = await _repository.GetSearchArtists();
        _cache.AddArtists(response);
    }

    private async Task FetchGenres()
    {
        var response = await _repository.GetSearchGenres();
        _cache.AddGenres(response);
    }

    private async Task FetchGroupings()
    {
        var response = await _repository.GetSearchGroupings();
        _cache.AddGroupings(response);
    }
}