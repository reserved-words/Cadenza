﻿using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Components.ViewBases;

public class ArtistViewBase : FluxorComponent
{
    [Parameter] public ArtistInfo Model { get; set; } = new();

    protected override void OnInitialized()
    {
        SubscribeToAction<ArtistUpdatedAction>(OnArtistUpdated);
        base.OnInitialized();
    }

    private void OnArtistUpdated(ArtistUpdatedAction action)
    {
        if (Model == null)
            return;

        if (Model.Id != action.ArtistId)
            return;

        action.Update.ApplyUpdates(Model);
        StateHasChanged();
    }
}
