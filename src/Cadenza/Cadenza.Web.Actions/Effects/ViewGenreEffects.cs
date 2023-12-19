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
        (var grouping, var genre) = action.Id.SplitGenreId();
        var vm = await _api.GetGenre(grouping, genre);
        dispatcher.Dispatch(new FetchViewGenreResult(vm));
    }
}
