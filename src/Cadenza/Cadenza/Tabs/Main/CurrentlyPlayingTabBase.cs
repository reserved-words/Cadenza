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
        Messenger.Subscribe<TrackStatusEventArgs>(OnTrackStatusChanged);
    }

    private async Task OnTrackStatusChanged(object sender, TrackStatusEventArgs e)
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