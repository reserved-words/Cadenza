using Cadenza.Common;

namespace Cadenza.Web.Actions.Effects;

public class ViewGenreEffects
{
    private readonly ILibraryApi _api;

    public ViewGenreEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewGenreRequest(FetchViewGenreRequest action, IDispatcher dispatcher)
    {
        var genre = await _api.GetGenre(action.Id);
        dispatcher.Dispatch(new FetchViewGenreResult(genre));
    }
}
