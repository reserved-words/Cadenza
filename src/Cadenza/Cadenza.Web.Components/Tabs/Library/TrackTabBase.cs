using Cadenza.Web.Model;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Components.Tabs.Library;

public class TrackTabBase : FluxorComponent
{
    [Inject] public IState<ViewTrackState> ViewTrackState { get; set; }

    public bool Loading => ViewTrackState.Value.IsLoading;
    public TrackFullVM Model => ViewTrackState.Value.Track;
}
