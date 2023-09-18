using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Web.Player;

public class PlayerBase : FluxorComponent
{
    [Inject]
    public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public bool Loading => CurrentTrackState.Value.IsLoading;

    //protected PlayTrack Track => PlaylistQueueState.Value.CurrentTrack;

    protected bool IsLastTrack => CurrentTrackState.Value.IsLastInPlaylist;

    protected TrackFull Model => CurrentTrackState.Value.FullTrack;

    protected bool Empty => Model == null && !Loading;
}
