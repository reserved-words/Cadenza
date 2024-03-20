namespace Cadenza.Features.Tabs.CurrentlyPlaying;

public partial class CurrentlyPlayingTab
{
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public TrackFullVM Model => CurrentTrackState.Value.Track;
}