namespace Cadenza.State.Actions.Effects;

public class ViewGenreEffects
{
    private readonly IArtistRepository _repository;

    public ViewGenreEffects(IArtistRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchViewGenreRequest(FetchViewGenreRequest action, IDispatcher dispatcher)
    {
        var artists = await _repository.GetArtistsByGenre(action.Genre);
        var orderedArtists = artists.OrderBy(a => a.Name).ToList();
        dispatcher.Dispatch(new FetchViewGenreResult(action.Genre, orderedArtists));
    }
}
