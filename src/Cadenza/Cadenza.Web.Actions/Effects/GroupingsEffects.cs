namespace Cadenza.Web.Actions.Effects;

public class GroupingsEffects
{
    private readonly IAdminApi _api;

    public GroupingsEffects(IAdminApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchGroupingsRequest(FetchGroupingsRequest action, IDispatcher dispatcher)
    {
        var result = await _api.GetGroupingOptions();
        dispatcher.Dispatch(new FetchGroupingsResult(result));
    }
}
