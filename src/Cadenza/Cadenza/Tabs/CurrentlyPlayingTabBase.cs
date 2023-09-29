using Cadenza.Common.Domain.Model.Library;
using Fluxor;

namespace Cadenza.Tabs;

public class CurrentlyPlayingTabBase : FluxorComponent
{
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public TrackFull Model => CurrentTrackState.Value.Track;
}