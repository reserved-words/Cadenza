using Cadenza.Common.Domain.Extensions;

namespace Cadenza.State.Actions.Effects;

public class ViewGroupingEffects
{
    private readonly IArtistRepository _repository;

    public ViewGroupingEffects(IArtistRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchViewGroupingRequest(FetchViewGroupingRequest action, IDispatcher dispatcher)
    {
        var artists = await _repository.GetArtistsByGrouping(action.Grouping.Id);
        var artistsByGenre = artists.ToGroupedDictionary(a => a.Genre ?? "None");
        var genres = artistsByGenre.Keys.ToList();
        dispatcher.Dispatch(new FetchViewGroupingResult(action.Grouping, genres));
    }
}
