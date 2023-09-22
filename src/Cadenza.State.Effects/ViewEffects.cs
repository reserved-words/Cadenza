namespace Cadenza.State.Effects;

public class ViewEffects
{
    [EffectMethod]
    public Task HandleViewItemRequest(ViewItemRequest action, IDispatcher dispatcher)
    {
        // Clear the other states e.g. clear grouping state when artist is fetched?
        // Note doing it here means the view itself will already have changed just before the data is loaded - maybe need to improve this

        if (action.Type == PlayerItemType.Artist)
        {
            dispatcher.Dispatch(new FetchViewArtistRequest(int.Parse(action.Id)));
        }
        else if (action.Type == PlayerItemType.Genre)
        {
            dispatcher.Dispatch(new FetchViewGenreRequest(action.Id));
        }
        else if (action.Type == PlayerItemType.Grouping)
        {
            dispatcher.Dispatch(new FetchViewGroupingRequest(new Grouping(int.Parse(action.Id), action.Name)));
        }

        return Task.CompletedTask;
    }
}
