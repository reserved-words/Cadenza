using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Web.Player;

public class PlayerBase : FluxorComponent
{
    [Inject]
    public IState<CurrentTrackState> CurrentTrackState { get; set; }

    [Inject]
    public IState<PlaylistQueueState> PlaylistQueueState { get; set; }

    public bool Loading => CurrentTrackState.Value.IsLoading;

    protected PlayTrack Track => PlaylistQueueState.Value.CurrentTrack;

    protected bool IsLastTrack => PlaylistQueueState.Value.IsCurrentTrackLast;

    protected TrackFull Model => CurrentTrackState.Value.Track;

    protected bool Empty => Model == null && !Loading;
}
