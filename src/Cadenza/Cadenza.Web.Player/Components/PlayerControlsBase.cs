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
        Dispatcher.Dispatch(new PlayerControlsPauseRequest());
    }

    protected void Resume()
    {
        CanPlay = false;
        CanPause = true;
        Dispatcher.Dispatch(new PlayerControlsResumeRequest());
    }

    public void SkipNext()
    {
        Dispatcher.Dispatch(new PlayerControlsNextRequest());
    }

    public void SkipPrevious()
    {
        Dispatcher.Dispatch(new PlayerControlsPreviousRequest());
    }
}
