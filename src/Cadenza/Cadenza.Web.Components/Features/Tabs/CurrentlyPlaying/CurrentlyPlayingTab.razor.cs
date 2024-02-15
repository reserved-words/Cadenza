namespace Cadenza.Web.Components.Features.Tabs.CurrentlyPlaying;

public class CurrentlyPlayingTabBase : FluxorComponent
{
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public TrackFullVM Model => CurrentTrackState.Value.Track;
}