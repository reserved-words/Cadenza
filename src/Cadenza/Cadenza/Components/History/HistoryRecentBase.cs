using Cadenza.Web.Common.Interfaces.Connections;

namespace Cadenza.Components.History;

public class HistoryRecentBase : ComponentBase
{
    [Inject]
    public IConnectionService ConnectorService { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IHistory History { get; set; }

    public List<RecentTrack> Model { get; set; }

    public bool IsLoading { get; set; } = true;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<TrackStatusEventArgs>(App_TrackProgressed);
    }

    private async Task App_TrackProgressed(object sender, TrackStatusEventArgs e)
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