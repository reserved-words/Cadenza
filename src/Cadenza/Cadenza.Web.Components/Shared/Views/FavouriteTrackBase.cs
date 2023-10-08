using Cadenza.Web.State.Actions;

namespace Cadenza.Web.Components.Shared.Views;

public class FavouriteTrackBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public string Artist { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public bool? IsFavourite { get; set; }

    public bool IsEnabled { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<IsFavouriteResultAction>(OnIsFavouriteResult);
        SubscribeToAction<FavouriteStatusChangedAction>(OnFavouriteStatusChanged);
        base.OnInitialized();
    }

    private void OnFavouriteStatusChanged(FavouriteStatusChangedAction action)
    {
        if (action.Artist != Artist || action.Title != Title)
            return;

        IsFavourite = action.IsFavourite;
    }

    private void OnIsFavouriteResult(IsFavouriteResultAction action)
    {
        if (action.Artist != Artist || action.Title != Title)
            return;

        IsFavourite = action.Result;
    }

    protected override void OnParametersSet()
    {
        IsEnabled = false;

        if (Artist == null || Title == null)
            return;

        IsEnabled = true;

        if (!IsFavourite.HasValue)
        {
            IsFavourite = false;
            Dispatcher.Dispatch(new IsFavouriteRequest(Artist, Title));
        }
    }

    public void Favourite()
    {
        Dispatcher.Dispatch(new FavouriteRequest(Artist, Title));
        IsFavourite = true;
    }

    public void Unfavourite()
    {
        Dispatcher.Dispatch(new UnfavouriteRequest(Artist, Title));
        IsFavourite = false;
    }
}
