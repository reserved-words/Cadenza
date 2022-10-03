namespace Cadenza.Web.Player.Components;

public class CurrentTrackProgressBase : ComponentBase
{
    [Inject]
    internal IMessenger Messenger { get; set; }

    public double Progress { get; set; }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<TrackFinishedEventArgs>(OnTrackFinished);
        Messenger.Subscribe<TrackProgressedEventArgs>(OnTrackProgressed);
    }

    private Task OnTrackFinished(object sender, TrackFinishedEventArgs e)
    {
        Progress = 0;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
