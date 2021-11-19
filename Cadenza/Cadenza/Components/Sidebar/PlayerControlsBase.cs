namespace Cadenza;

public class PlayerControlsBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Parameter]
    public Func<Task> OnResume { get; set; }

    [Parameter]
    public Func<Task> OnPause { get; set; }

    [Parameter]
    public Func<Task> OnSkipNext { get; set; }

    [Parameter]
    public Func<Task> OnSkipPrevious { get; set; }

    public bool CanSkipNext { get; set; }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipPrevious => CanPlay || CanPause;

    protected override async Task OnInitializedAsync()
    {
        App.TrackStarted += App_TrackStarted;
        App.TrackPaused += App_TrackPaused;
        App.TrackResumed += App_TrackResumed;
    }

    private async Task App_TrackStarted(object sender, TrackEventArgs e)
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
    }

    private async Task App_TrackResumed(object sender, TrackEventArgs e)
    {
        CanPause = true;
        CanPlay = false;
    }

    private async Task App_TrackPaused(object sender, TrackEventArgs e)
    {
        CanPlay = true;
        CanPause = false;
    }
}