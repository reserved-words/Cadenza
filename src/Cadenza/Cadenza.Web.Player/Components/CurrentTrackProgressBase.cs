using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Web.Player.Components;

public class CurrentTrackProgressBase : FluxorComponent
{
    [Inject]
    internal IMessenger Messenger { get; set; }

    [Inject]
    private IState<CurrentTrackState> CurrentTrackState { get; set; }

    public double Progress { get; set; }

    public int SecondsPlayed { get; set; }

    public int SecondsRemaining { get; set; }

    protected override void OnInitialized()
    {
        //Messenger.Subscribe<TrackFinishedEventArgs>(OnTrackFinished);
        Messenger.Subscribe<TrackProgressedEventArgs>(OnTrackProgressed);

        CurrentTrackState.StateChanged += CurrentTrackState_StateChanged;
    }

    private void CurrentTrackState_StateChanged(object sender, EventArgs e)
    {
        if (CurrentTrackState.Value.PlayTrack == null)
        {
            SecondsPlayed = 0;
            SecondsRemaining = 0;
            Progress = 0;
            StateHasChanged();
        }
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
