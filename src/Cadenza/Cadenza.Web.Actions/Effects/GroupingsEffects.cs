namespace Cadenza.Web.Actions.Effects;

public class GroupingsEffects
{
    private readonly ILibraryApi _api;

    public GroupingsEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchGroupingsRequest(FetchGroupingsRequest action, IDispatcher dispatcher)
    {
        var result = await _api.GetGroupings();
        dispatcher.Dispatch(new FetchGroupingsResult(result));
    }
}
