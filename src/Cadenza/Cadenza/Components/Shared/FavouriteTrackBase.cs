using Cadenza.Common;

namespace Cadenza.Components.Shared;

public class FavouriteTrackBase : ComponentBase
{
    [Inject]
    public IFavouritesConsumer Favourites { get; set; }

    [Inject]
    public IFavouritesController FavouritesController { get; set; }

    [Parameter]
    public string Artist { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public bool? IsFavourite { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (!IsFavourite.HasValue)
        {
            IsFavourite = false;
            IsFavourite = await Favourites.IsFavourite(Artist, Title);
        }
    }

    public async Task Favourite()
    {
        await FavouritesController.Favourite(Artist, Title);
        IsFavourite = true;
    }

    public async Task Unfavourite()
    {
        await FavouritesController.Unfavourite(Artist, Title);
        IsFavourite = false;
    }
}
