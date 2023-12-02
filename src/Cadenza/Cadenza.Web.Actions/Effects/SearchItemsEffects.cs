using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Actions.Effects;

public class SearchItemsEffects
{
    private readonly ISearchApi _api;

    public SearchItemsEffects(ISearchApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleSearchItemsUpdateRequest(SearchItemsUpdateRequest action, IDispatcher dispatcher)
    {
        // Do these all at the same time

        var items = new List<SearchItemVM>();

        var tracks = await _api.GetTracks();
        items.AddRange(tracks);

        var albums = await _api.GetAlbums();
        items.AddRange(albums);

        var artists = await _api.GetArtists();
        items.AddRange(artists);

        var genres = await _api.GetGenres();
        items.AddRange(genres);

        var groupings = await _api.GetGroupings();
        items.AddRange(groupings);

        var tags = await _api.GetTags();
        items.AddRange(tags);

        dispatcher.Dispatch(new SearchItemsUpdatedAction(items));
    }
}
