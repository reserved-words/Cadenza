using Cadenza.Web.Common.Interfaces.Searchbar;

namespace Cadenza.Web.Core.Services;

internal class SearchSyncService : ISearchSyncService
{
    private readonly ISearchRepository _repository;

    public SearchSyncService(ISearchRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PlayerItem>> GetSearchItems()
    {
        var items = new List<PlayerItem>();

        var tracks = await _repository.GetTracks();
        items.AddRange(tracks);

        var albums = await _repository.GetSearchAlbums();
        items.AddRange(albums);

        var artists = await _repository.GetArtists();
        items.AddRange(artists);

        var genres = await _repository.GetGenres();
        items.AddRange(genres);

        var groupings = await _repository.GetGroupings();
        items.AddRange(groupings);

        var tags = await _repository.GetTags();
        items.AddRange(tags);

        return items;
    }
}