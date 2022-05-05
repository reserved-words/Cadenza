﻿using Cadenza.Core.App;
using Cadenza.Interfaces;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.Components.Shared.Menus;

public class MenuGroupingBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

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

    public Task OnEdit()
    {
        return Task.CompletedTask;
    }

    public async Task OnPlay()
    {
        await Player.PlayGrouping(Id.Parse<Grouping>());
    }
}
