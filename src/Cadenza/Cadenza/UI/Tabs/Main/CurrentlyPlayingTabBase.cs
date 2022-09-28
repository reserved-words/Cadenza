using Cadenza.Domain.Model.Track;
using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Interop;
using Cadenza.Web.Core.Events;
using Cadenza.Web.Core.Interfaces;

namespace Cadenza.UI.Tabs.Main;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IStoreGetter Store { get; set; }

    public TrackFull Model { get; set; } = new();

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        Model = (await Store.GetValue<TrackFull>(StoreKey.CurrentTrack)).Value;
        StateHasChanged();
    }

    private Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        Model = null;
        StateHasChanged();
        return Task.CompletedTask;
    }
}