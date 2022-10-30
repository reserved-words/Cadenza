namespace Cadenza.Web.Player.Components;

public class CurrentTrackProgressBase : ComponentBase
{
    [Inject]
    internal IMessenger Messenger { get; set; }

    public double Progress { get; set; }

    public int SecondsPlayed { get; set; }

    public int SecondsRemaining { get; set; }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<TrackFinishedEventArgs>(OnTrackFinished);
        Messenger.Subscribe<TrackProgressedEventArgs>(OnTrackProgressed);
    }

    private Task OnTrackFinished(object sender, TrackFinishedEventArgs e)
    {
        SecondsPlayed = 0;
        SecondsRemaining = 0;
        Progress = 0;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        SecondsPlayed = e.SecondsPlayed;
        SecondsRemaining = e.SecondsPlayed - e.TotalSeconds;
        Progress = e.ProgressPercentage;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
