using Cadenza.Core.App;
using Cadenza.Core.CurrentlyPlaying;

namespace Cadenza.Components.Sidebar;

public class PlayerControlsBase : ComponentBase
{
    [Inject]
    public IAppConsumer AppConsumer { get; set; }

    [Inject]
    public IAppController AppController { get; set; }

    public bool CanSkipNext { get; set; }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipPrevious => CanPlay || CanPause;

    public async Task OnPause()
    {
        await AppController.Pause();
    }

    public async Task OnResume()
    {
        await AppController.Resume();
    }

    public async Task OnSkipNext()
    {
        await AppController.SkipNext();
    }

    public async Task OnSkipPrevious()
    {
        await AppController.SkipPrevious();
    }

    protected override void OnInitialized()
    {
        AppConsumer.TrackStarted += App_TrackStarted;
        AppConsumer.TrackPaused += App_TrackPaused;
        AppConsumer.TrackResumed += App_TrackResumed;
    }

    private Task App_TrackStarted(object sender, TrackEventArgs e)
    {
        if (e.CurrentTrack != null)
        {
            CanPause = true;
            CanPlay = false;
        }
        else
        {
            CanPause = false;
            CanPlay = true;
        }

        CanSkipNext = !e.IsLastTrack;

        StateHasChanged();

        return Task.CompletedTask;
    }

    private Task App_TrackResumed(object sender, TrackEventArgs e)
    {
        CanPause = true;
        CanPlay = false;
        return Task.CompletedTask;
    }

    private Task App_TrackPaused(object sender, TrackEventArgs e)
    {
        CanPlay = true;
        CanPause = false;
        return Task.CompletedTask;
    }
}