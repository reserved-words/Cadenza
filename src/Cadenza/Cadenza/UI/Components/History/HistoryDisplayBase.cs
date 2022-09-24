using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Core.Interfaces;

namespace Cadenza.UI.Components.History;

public abstract class HistoryDisplayBase<T> : ComponentBase
{
    [Inject]
    public IConnectorConsumer ConnectorService { get; set; }

    [Inject]
    public IHistory History { get; set; }

    protected List<T> Model { get; set; }
    protected bool IsLoading { get; set; } = true;
    protected abstract Task<List<T>> GetItems(HistoryPeriod period);

    protected override void OnInitialized()
    {
        ConnectorService.ConnectorStatusChanged += OnConnectorStatusChanged;
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

        Model = await GetItems(HistoryPeriod.Week);
        IsLoading = false;
    }
}