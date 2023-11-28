namespace Cadenza.Web.Actions.Effects;

public class FavouritesEffects
{
    private readonly IFavouritesService _favourites;

    public FavouritesEffects(IFavouritesService favourites)
    {
        _favourites = favourites;
    }

    [EffectMethod]
    public async Task HandleFavouriteRequest(FavouriteRequest action, IDispatcher dispatcher)
    {
        await _favourites.Favourite(action.TrackId);
        dispatcher.Dispatch(new FavouriteStatusChangedAction(action.TrackId, true));
    }

    [EffectMethod]
    public async Task HandleUnfavouriteRequest(UnfavouriteRequest action, IDispatcher dispatcher)
    {
        await _favourites.Unfavourite(action.TrackId);
        dispatcher.Dispatch(new FavouriteStatusChangedAction(action.TrackId, false));
    }
}
