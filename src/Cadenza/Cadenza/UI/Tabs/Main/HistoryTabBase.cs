using Cadenza.Core.Common;
using Cadenza.Core.Interfaces;
using Cadenza.LastFM;

namespace Cadenza.UI.Tabs.Main;

public class HistoryTabBase : ComponentBase
{
    [Inject]
    public IConnectorConsumer ConnectorService { get; set; }

    [Inject]
    public IHistory History { get; set; }

    public List<PlayedTrack> TopTracks { get; private set; }

    public List<PlayedAlbum> TopAlbums { get; private set; }

    public List<PlayedArtist> TopArtists { get; private set; }

    public bool IsLoading { get; private set; } = true;
    public bool IsPopulated { get; private set; }

    protected override void OnInitialized()
    {
        //ConnectorService.ConnectorStatusChanged += OnConnectorStatusChanged;
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
        if (IsPopulated == true)
            return;

        IsLoading = true;

        if (status == ConnectorStatus.Loading)
            return;

        if (status == ConnectorStatus.Errored || status == ConnectorStatus.Disabled)
        {
            IsLoading = false;
            return;
        }

        TopArtists = await GetArtists(HistoryPeriod.Week);
        TopAlbums = await GetAlbums(HistoryPeriod.Week);
        TopTracks = await GetTracks(HistoryPeriod.Week);

        IsLoading = false;

        IsPopulated = true;
    }

    protected async Task<List<PlayedArtist>> GetArtists(HistoryPeriod period)
    {
        return (await History.GetPlayedArtists(period, 5, 1)).ToList();
    }

    protected async Task<List<PlayedAlbum>> GetAlbums(HistoryPeriod period)
    {
        return (await History.GetPlayedAlbums(period, 5, 1)).ToList();
    }

    protected async Task<List<PlayedTrack>> GetTracks(HistoryPeriod period)
    {
        return (await History.GetPlayedTracks(period, 5, 1)).ToList();
    }
}