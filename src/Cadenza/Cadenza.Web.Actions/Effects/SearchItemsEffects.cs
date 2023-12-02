namespace Cadenza.Web.Actions.Effects;

public class SearchItemsEffects
{
    private readonly ISearchRepository _repository;

    public SearchItemsEffects(ISearchRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleSearchItemsUpdateRequest(SearchItemsUpdateRequest action, IDispatcher dispatcher)
    {
        // Do these all at the same time

        var items = new List<SearchItemVM>();

        var tracks = await _repository.GetTracks();
        items.AddRange(tracks);

        var albums = await _repository.GetAlbums();
        items.AddRange(albums);

        var artists = await _repository.GetArtists();
        items.AddRange(artists);

        var genres = await _repository.GetGenres();
        items.AddRange(genres);

        var groupings = await _repository.GetGroupings();
        items.AddRange(groupings);

        var tags = await _repository.GetTags();
        items.AddRange(tags);

        dispatcher.Dispatch(new SearchItemsUpdatedAction(items));
    }
}
