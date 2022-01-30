using Cadenza.API.Core.LastFM;

namespace Cadenza;

public class HistoryRecentBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IHistory History { get; set; }

    public List<RecentTrack> Model { get; set; }

    public bool IsLoading { get; set; } = true;

    protected override void OnInitialized()
    {
        // App.TrackFinished += App_TrackProgressed;
        App.TrackPaused += App_TrackProgressed;
        App.TrackResumed += App_TrackProgressed;
        App.TrackStarted += App_TrackProgressed;
    }

    private async Task App_TrackProgressed(object sender, TrackEventArgs e)
    {
        await Task.Delay(1000).ContinueWith(async t =>
        {
            await Update();
            StateHasChanged();
        });
    }

    protected override async Task OnParametersSetAsync()
    {
        IsLoading = true;
        await Update();
        IsLoading = false;
    }

    private async Task Update()
    {
        Model = (await History.GetRecentTracks(20, 1)).ToList();
    }
}