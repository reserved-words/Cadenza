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
    }

    private async Task OnPlayStatusUpdated(object sender, PlayStatusEventArgs e)
    {
        if (e.Status == PlayStatus.Stopped)
        {
            Model = null;
        }
        else if (Model == null || Model.Track.Id != e.Track.Id)
        {
            Model = await Store.GetCurrentTrack();
        }

        StateHasChanged();
    }
}