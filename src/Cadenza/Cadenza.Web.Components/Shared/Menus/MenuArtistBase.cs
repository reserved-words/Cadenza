﻿using Cadenza.Web.Common.Interfaces.Play;
using IDialogService = Cadenza.Web.Components.Interfaces.IDialogService;

namespace Cadenza.Web.Components.Shared.Menus;

public class MenuArtistBase : ComponentBase
{
    [Inject]
    public IArtistRepository Repository { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string Id { get; set; }

    public async Task OnEdit()
    {
        var artist = await Repository.GetArtist(Id);
        await DialogService.DisplayForm<EditArtist, ArtistInfo>(artist, "Edit Artist");
    }

    public async Task OnPlay()
    {
        await Player.PlayArtist(Id);
    }
}
