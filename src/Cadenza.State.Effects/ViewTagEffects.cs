namespace Cadenza.State.Effects;

public class ViewTagEffects
{
    private readonly ITagRepository _repository;

    public ViewTagEffects(ITagRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchViewTagRequest(FetchViewTagRequest action, IDispatcher dispatcher)
    {
        var items = (await _repository.GetTag(action.Tag))
            .OrderBy(i => i.Type)
            .ThenBy(i => i.Name)
            .ToList();

        dispatcher.Dispatch(new FetchViewTagResult(action.Tag, items));
    }
}
