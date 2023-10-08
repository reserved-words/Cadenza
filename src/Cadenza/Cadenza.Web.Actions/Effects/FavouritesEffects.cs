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
        await _favourites.Favourite(action.Artist, action.Title);
        dispatcher.Dispatch(new FavouriteStatusChangedAction(action.Artist, action.Title, true));
    }

    [EffectMethod]
    public async Task HandleIsFavouriteRequest(IsFavouriteRequest action, IDispatcher dispatcher)
    {
        var result = await _favourites.IsFavourite(action.Artist, action.Title);
        dispatcher.Dispatch(new IsFavouriteResultAction(action.Artist, action.Title, result));
    }

    [EffectMethod]
    public async Task HandleUnfavouriteRequest(UnfavouriteRequest action, IDispatcher dispatcher)
    {
        await _favourites.Unfavourite(action.Artist, action.Title);
        dispatcher.Dispatch(new FavouriteStatusChangedAction(action.Artist, action.Title, false));
    }
}
