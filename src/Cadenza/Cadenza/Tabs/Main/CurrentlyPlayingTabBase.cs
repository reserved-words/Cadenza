using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Components.Tabs.Main;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public ICurrentTrackStore Store { get; set; }

    public TrackFull Model { get; set; } = new();

    protected override void OnInitialized()
    {
        Messenger.Subscribe<PlayStatusEventArgs>(OnPlayStatusUpdated);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished);
    }

    private async Task OnPlayStatusUpdated(object sender, PlayStatusEventArgs e)
    {
        if (e.Track == null)
            return;

        if (Model?.Track.Id == e.Track.Id)
            return;

        Model = await Store.GetCurrentTrack();
        StateHasChanged();
    }

    private Task OnPlaylistFinished(object arg1, PlaylistFinishedEventArgs arg2)
    {
        Model = null;
        StateHasChanged();
        return Task.CompletedTask;
    }
}