using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Web.Player;

public class PlayerBase : FluxorComponent
{
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    protected override void OnInitialized()
    {
        CurrentTrackState.StateChanged += OnCurrentTrackChanged;

        base.OnInitialized();
    }

    protected bool Loading => CurrentTrackState.Value.IsLoading;
    protected bool IsLastTrack => CurrentTrackState.Value.IsLastInPlaylist;
    protected bool IsTrackPopulated => Model != null;
    protected TrackFull Model => CurrentTrackState.Value.Track;

    protected bool Empty => Model == null && !Loading;

    private void OnCurrentTrackChanged(object sender, EventArgs e)
    {
        StateHasChanged();
    }
}
