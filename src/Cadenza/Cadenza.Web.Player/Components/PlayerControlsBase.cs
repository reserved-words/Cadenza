﻿using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Player.Components;

public class PlayerControlsBase : ComponentBase
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public bool IsTrackPopulated { get; set; }
    [Parameter] public bool IsLastTrack { get; set; }

    protected bool CanPause { get; set; }
    protected bool CanPlay { get; set; }

    protected bool CanSkipNext => IsTrackPopulated && !IsLastTrack;
    protected bool CanSkipPrevious => IsTrackPopulated;

    protected override void OnParametersSet()
    {
        CanPlay = false;
        CanPause = IsTrackPopulated;
    }

    protected void Pause()
    {
        CanPlay = true;
        CanPause = false;
        OnPause();
    }

    protected void Resume()
    {
        CanPlay = false;
        CanPause = true;
        OnResume();
    }

    public void SkipNext()
    {
        Dispatcher.Dispatch(new PlayerControlsNextRequest());
    }

    public void SkipPrevious()
    {
        Dispatcher.Dispatch(new PlayerControlsPreviousRequest());
    }

    protected void OnPause()
    {
        Dispatcher.Dispatch(new PlayerControlsPauseRequest());
    }

    protected void OnResume()
    {
        Dispatcher.Dispatch(new PlayerControlsResumeRequest());
    }
}
