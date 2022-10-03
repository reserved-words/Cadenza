﻿using Cadenza.Web.Common.Interfaces.Connections;
using Cadenza.Web.Common.Interfaces.Favourites;

namespace Cadenza.Web.Components.Shared.Views;

public class FavouriteTrackBase : ComponentBase
{
    [Inject]
    public IFavouritesMessenger Favourites { get; set; }

    [Inject]
    public IFavouritesController FavouritesController { get; set; }

    [Inject]
    public IConnectionService ConnectorService { get; set; }

    [Parameter]
    public string Artist { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public bool? IsFavourite { get; set; }

    public bool IsEnabled { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        IsEnabled = false;

        var status = ConnectorService.GetStatus(Connector.LastFm);

        if (status != ConnectorStatus.Connected)
            return;

        if (Artist == null || Title == null)
            return;

        IsEnabled = true;

        if (!IsFavourite.HasValue)
        {
            IsFavourite = false;
            IsFavourite = await Favourites.IsFavourite(Artist, Title);
        }

        StateHasChanged();
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