namespace Cadenza.Web.Actions.Effects;

public class ViewTagEffects
{
    private readonly ILibraryApi _api;

    public ViewTagEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewTagRequest(FetchViewTagRequest action, IDispatcher dispatcher)
    {
        var items = (await _api.GetTag(action.Tag))
            .OrderBy(i => i.Type)
            .ThenBy(i => i.Name)
            .ToList();

        dispatcher.Dispatch(new FetchViewTagResult(action.Tag, items));
    }
}
