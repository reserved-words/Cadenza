using Fluxor;

namespace Cadenza.Web.Components.Tabs;

public class CurrentlyPlayingTabBase : FluxorComponent
{
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public TrackFull Model => CurrentTrackState.Value.Track;
}