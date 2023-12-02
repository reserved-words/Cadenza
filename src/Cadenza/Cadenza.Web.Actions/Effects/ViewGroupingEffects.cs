namespace Cadenza.Web.Actions.Effects;

public class ViewGroupingEffects
{
    private readonly IArtistApi _api;

    public ViewGroupingEffects(IArtistApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewGroupingRequest(FetchViewGroupingRequest action, IDispatcher dispatcher)
    {
        var artists = await _api.GetArtistsByGrouping(action.Grouping.Id);
        var artistsByGenre = ToGroupedDictionary(artists, a => a.Genre ?? "None");
        var genres = artistsByGenre.Keys.ToList();
        dispatcher.Dispatch(new FetchViewGroupingResult(action.Grouping, genres));
    }

    private static Dictionary<TKey, List<TValue>> ToGroupedDictionary<TKey, TValue>(List<TValue> items, Func<TValue, TKey> getKey)
    {
        return items
            .OrderBy(i => getKey(i))
            .GroupBy(i => getKey(i))
            .ToDictionary(g => g.Key, g => g.ToList());
    }
}
