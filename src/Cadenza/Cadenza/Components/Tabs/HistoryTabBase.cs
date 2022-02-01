using Cadenza.API.Core.LastFM;

namespace Cadenza;

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

        TopTracks = (await History.GetPlayedTracks(HistoryPeriod.Week, 5, 1)).ToList();
        TopAlbums = (await History.GetPlayedAlbums(HistoryPeriod.Week, 5, 1)).ToList();
        TopArtists = (await History.GetPlayedArtists(HistoryPeriod.Week, 5, 1)).ToList();
        IsLoading = false;

        IsPopulated = true;
    }
}