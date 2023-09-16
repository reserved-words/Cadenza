using Cadenza.State.Store;
using Cadenza.Web.Common.Interfaces.Connections;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Components.History;

public class HistoryRecentBase : FluxorComponent
{
    // TODO: currently it will check first if the last.FM status is errored or disconnected to avoid problems - how to handle this now?
    // Doesn't seem great to have to check another state in the effect of one state change?
    // TODO: MaxItems was previously provided as a parameter - how to handle this now?
    // TODO: Also note that FetchRecentPlayHistoryAction needs to be triggered when the app is loaded, not only when the play status changes



    [Inject]
    public IConnectionService ConnectorService { get; set; }

    //[Inject]
    //public IMessenger Messenger { get; set; }

    //[Inject]
    //public IHistory History { get; set; }

    [Inject]
    public IState<RecentPlayHistoryState> RecentPlayHistoryState { get; set; }

    [Parameter]
    public int MaxItems { get; set; }

    public List<RecentTrack> Model => RecentPlayHistoryState.Value.Tracks;

    public bool IsLoading => RecentPlayHistoryState.Value.IsLoading;

    //protected override void OnInitialized()
    //{
    //    //Messenger.Subscribe<PlayStatusEventArgs>(OnPlayStatusUpdated);
    //    PlayStatusState.StateChanged += PlayStatusState_StateChanged;
    //}

    //private void PlayStatusState_StateChanged(object sender, EventArgs e)
    //{
    //    var status = ConnectorService.GetStatus(Connector.LastFm);
    //    await LoadData(status);
    //    StateHasChanged();

    //    //await Task.Delay(1000).ContinueWith(async t =>
    //    //{
    //    //    var status = ConnectorService.GetStatus(Connector.LastFm);
    //    //    await LoadData(status);
    //    //    StateHasChanged();
    //    //});
    //}

    //private async Task OnPlayStatusUpdated(object sender, PlayStatusEventArgs e)
    //{
    //    await Task.Delay(1000).ContinueWith(async t =>
    //    {
    //        var status = ConnectorService.GetStatus(Connector.LastFm);
    //        await LoadData(status);
    //        StateHasChanged();
    //    });
    //}

    //private async Task OnConnectorStatusChanged(object sender, ConnectorEventArgs e)
    //{
    //    if (e.Connector != Connector.LastFm)
    //        return;

    //    await LoadData(e.Status);
    //}

    //protected override async Task OnParametersSetAsync()
    //{
    //    var status = ConnectorService.GetStatus(Connector.LastFm);
    //    await LoadData(status);
    //}

    //private async Task LoadData(ConnectorStatus status)
    //{
    //    if (status == ConnectorStatus.Errored || status == ConnectorStatus.Disabled)
    //    {
    //        IsLoading = false;
    //        return;
    //    }

    //    IsLoading = true;

    //    if (status == ConnectorStatus.Loading)
    //        return;

    //    Model = (await History.GetRecentTracks(MaxItems, 1)).ToList();
    //    IsLoading = false;
    //}
}