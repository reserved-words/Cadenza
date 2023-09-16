using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Tabs;

public class CurrentlyPlayingTabBase : FluxorComponent
{
    [Inject]
    public IMessenger Messenger { get; set; }

    //[Inject]
    //public ICurrentTrackStore Store { get; set; }

    [Inject]
    public IState<CurrentTrackState> CurrentTrackState { get; set; }

    public TrackFull Model => CurrentTrackState.Value.Track;

    protected override void OnInitialized()
    {
        //Messenger.Subscribe<PlayStatusEventArgs>(OnPlayStatusUpdated);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished);
    }

    //private async Task OnPlayStatusUpdated(object sender, PlayStatusEventArgs e)
    //{
    //    if (e.Track == null)
    //        return;

    //    if (Model?.Track.Id == e.Track.Id)
    //        return;

    //    Model = await Store.GetCurrentTrack();
    //    StateHasChanged();
    //}

    private Task OnPlaylistFinished(object arg1, PlaylistFinishedEventArgs arg2)
    {
        //Model = null;
        StateHasChanged();
        return Task.CompletedTask;
    }
}