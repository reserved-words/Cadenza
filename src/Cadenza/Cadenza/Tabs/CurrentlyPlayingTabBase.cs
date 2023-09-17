using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Tabs;

public class CurrentlyPlayingTabBase : FluxorComponent
{
    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public TrackFull Model => CurrentTrackState.Value.FullTrack;
}