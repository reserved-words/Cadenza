namespace Cadenza.Web.Player.Components;

public class PlayerControlsBase : ComponentBase
{
    [Inject] public IMessenger Messenger { get; set; }

    [Parameter] public bool IsTrackPopulated { get; set; }
    [Parameter] public bool IsLastTrack { get; set; }

    [Parameter] public Func<Task> OnPause { get; set; }
    [Parameter] public Func<Task> OnResume { get; set; }

    protected bool CanPause { get; set; }
    protected bool CanPlay { get; set; }

    protected bool CanSkipNext => IsTrackPopulated && !IsLastTrack;
    protected bool CanSkipPrevious => IsTrackPopulated;

    protected override void OnParametersSet()
    {
        CanPlay = false;
        CanPause = IsTrackPopulated;
    }

    protected async Task Pause()
    {
        CanPlay = true;
        CanPause = false;
        await OnPause();
    }

    protected async Task Resume()
    {
        CanPlay = false;
        CanPause = true;
        await OnResume();
    }

    public async Task SkipNext()
    {
        await Messenger.Send(this, new SkipNextTrackEventArgs());
    }

    public async Task SkipPrevious()
    {
        await Messenger.Send(this, new SkipPreviousTrackEventArgs());
    }
}
