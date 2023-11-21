﻿namespace Cadenza.Web.Components.Player;

public class PlayerBase : FluxorComponent
{
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    protected override void OnInitialized()
    {
        CurrentTrackState.StateChanged += OnCurrentTrackChanged;

        base.OnInitialized();
    }

    protected bool Loading => CurrentTrackState.Value.IsLoading;
    protected bool IsLastTrack => CurrentTrackState.Value.IsLastInPlaylist;
    protected bool IsTrackPopulated => Model != null;
    protected TrackFullVM Model => CurrentTrackState.Value.Track;

    protected bool Empty => Model == null && !Loading;

    private void OnCurrentTrackChanged(object sender, EventArgs e)
    {
        StateHasChanged();
    }
}