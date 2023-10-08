using Cadenza.Web.Model;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Components.Tabs;

public class CurrentlyPlayingTabBase : FluxorComponent
{
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public TrackFullVM Model => CurrentTrackState.Value.Track;
}