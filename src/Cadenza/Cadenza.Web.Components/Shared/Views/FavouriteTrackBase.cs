namespace Cadenza.Web.Components.Shared.Views;

public class FavouriteTrackBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public int TrackId { get; set; }
    [Parameter] public bool IsFavourite { get; set; }

    public bool IsEnabled => TrackId > 0;

    protected override void OnInitialized()
    {
        SubscribeToAction<FavouriteStatusChangedAction>(OnFavouriteStatusChanged);
        base.OnInitialized();
    }

    private void OnFavouriteStatusChanged(FavouriteStatusChangedAction action)
    {
        if (action.TrackId != TrackId)
            return;

        IsFavourite = action.IsFavourite;
    }

    public void Favourite()
    {
        Dispatcher.Dispatch(new FavouriteRequest(TrackId));
        IsFavourite = true;
    }

    public void Unfavourite()
    {
        Dispatcher.Dispatch(new UnfavouriteRequest(TrackId));
        IsFavourite = false;
    }
}
