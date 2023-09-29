﻿using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Components.ViewBases;

public class TrackViewBase : FluxorComponent
{
    [Parameter] public TrackDetails Model { get; set; } = new();

    protected override void OnInitialized()
    {
        SubscribeToAction<TrackUpdatedAction>(OnTrackUpdated);
        base.OnInitialized();
    }

    private void OnTrackUpdated(TrackUpdatedAction action)
    {
        if (action.Update.Id == Model.Id)
        {
            action.Update.ApplyUpdates(Model);
            StateHasChanged();
        }
    }
}
