using Cadenza.Core;

namespace Cadenza;

public class CurrentTrackBase : ComponentBase
{
    [Inject]
    public ITrackProgressedConsumer TrackProgressConsumer { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IStoreGetter Store { get; set; }

    public double Progress { get; set; }

    public TrackSummary Model { get; set; }

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        TrackProgressConsumer.TrackProgressed += OnTrackProgressed;
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        Model = await Store.GetValue<TrackSummary>(StoreKey.CurrentTrack);
        StateHasChanged();
    }

    private void OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
    }
}
