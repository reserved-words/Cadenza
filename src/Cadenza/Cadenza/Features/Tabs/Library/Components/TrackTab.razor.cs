﻿namespace Cadenza.Features.Tabs.Library.Components;

public class TrackTabBase : FluxorComponent
{
    [Inject] public IState<ViewTrackState> ViewTrackState { get; set; }

    public bool Loading => ViewTrackState.Value.IsLoading;
    public TrackFullVM Model => ViewTrackState.Value.Track;
}
