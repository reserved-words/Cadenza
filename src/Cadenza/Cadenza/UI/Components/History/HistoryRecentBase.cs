using Cadenza.Core.Common;
using Cadenza.Core.App;
using Cadenza.Core.CurrentlyPlaying;
using Cadenza.Core.Interfaces;
using Cadenza.LastFM;

namespace Cadenza.UI.Components.History;

public class HistoryRecentBase : ComponentBase
{
    [Inject]
    public IConnectorConsumer ConnectorService { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IHistory History { get; set; }

    public List<RecentTrack> Model { get; set; }

    public bool IsLoading { get; set; } = true;

    protected override void OnInitialized()
    {
        //ConnectorService.ConnectorStatusChanged += OnConnectorStatusChanged;
        App.TrackPaused += App_TrackProgressed;
        App.TrackResumed += App_TrackProgressed;
        App.TrackStarted += App_TrackProgressed;
    }

    private async Task App_TrackProgressed(object sender, TrackEventArgs e)
    {
        await Task.Delay(1000).ContinueWith(async t =>
        {
            var status = ConnectorService.GetStatus(Connector.LastFm);
            await LoadData(status);
            StateHasChanged();
        });
    }

    private async Task OnConnectorStatusChanged(object sender, ConnectorEventArgs e)
    {
        if (e.Connector != Connector.LastFm)
            return;

        await LoadData(e.Status);
    }

    protected override async Task OnParametersSetAsync()
    {
        var status = ConnectorService.GetStatus(Connector.LastFm);
        await LoadData(status);
    }

    private async Task LoadData(ConnectorStatus status)
    {
        if (status == ConnectorStatus.Errored || status == ConnectorStatus.Disabled)
        {
            IsLoading = false;
            return;
        }

        IsLoading = true;

        if (status == ConnectorStatus.Loading)
            return;

        Model = (await History.GetRecentTracks(20, 1)).ToList();
        IsLoading = false;
    }
}