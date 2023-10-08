using Cadenza.Web.State.Actions;

namespace Cadenza.Web.Actions.Effects;

public class GroupingsEffects
{
    private readonly IAdminRepository _repository;

    public GroupingsEffects(IAdminRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchGroupingsRequest(FetchGroupingsRequest action, IDispatcher dispatcher)
    {
        var result = await _repository.GetGroupingOptions();
        dispatcher.Dispatch(new FetchGroupingsResult(result));
    }
}
