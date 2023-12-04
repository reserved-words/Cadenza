namespace Cadenza.Web.Actions.Effects;

public class FavouritesEffects
{
    private readonly IFavouritesApi _api;

    public FavouritesEffects(IFavouritesApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFavouriteRequest(FavouriteRequest action, IDispatcher dispatcher)
    {
        await _api.Favourite(action.TrackId);
        dispatcher.Dispatch(new FavouriteStatusChangedAction(action.TrackId, true));
    }

    [EffectMethod]
    public async Task HandleUnfavouriteRequest(UnfavouriteRequest action, IDispatcher dispatcher)
    {
        await _api.Unfavourite(action.TrackId);
        dispatcher.Dispatch(new FavouriteStatusChangedAction(action.TrackId, false));
    }
}
