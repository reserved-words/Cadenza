using Fluxor;

namespace Cadenza.Tabs.Library;

public class TrackTabBase : FluxorComponent
{
    [Inject] public IState<ViewTrackState> ViewTrackState { get; set; }

    public bool Loading => ViewTrackState.Value.IsLoading;
    public TrackFull Model => ViewTrackState.Value.Track;
}
