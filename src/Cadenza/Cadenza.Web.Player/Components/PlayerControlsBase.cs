using Cadenza.State.Actions;
using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.Web.Player.Components;

public class PlayerControlsBase : ComponentBase
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    [Parameter] public bool IsTrackPopulated { get; set; }
    [Parameter] public bool IsLastTrack { get; set; }

    protected bool CanPause { get; set; }
    protected bool CanPlay { get; set; }

    protected bool CanSkipNext => IsTrackPopulated && !IsLastTrack;
    protected bool CanSkipPrevious => IsTrackPopulated;

    protected override void OnParametersSet()
    {
        CanPlay = false;
        CanPause = IsTrackPopulated;
    }

    protected void Pause()
    {
        CanPlay = true;
        CanPause = false;
        OnPause();
    }

    protected void Resume()
    {
        CanPlay = false;
        CanPause = true;
        OnResume();
    }

    public void SkipNext()
    {
        // TODO: Change this to a PlayerControls action
        Dispatcher.Dispatch(new PlaylistMoveNextRequest());
    }

    public void SkipPrevious()
    {
        // TODO: Change this to a PlayerControls action
        Dispatcher.Dispatch(new PlaylistMovePreviousRequest());
    }



    protected void OnPause()
    {
        // TODO: Note for now have to use current track state here - change this so that this is a PlayerControlsRequest and handler of that sorts getting current track
        Dispatcher.Dispatch(new PlayerPauseRequest(CurrentTrackState.Value.Track));
    }

    protected void OnResume()
    {
        // TODO: Note for now have to use current track state here - change this so that this is a PlayerControlsRequest and handler of that sorts getting current track
        Dispatcher.Dispatch(new PlayerResumeRequest(CurrentTrackState.Value.Track));
    }
}
