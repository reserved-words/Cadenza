using Cadenza.Web.Common.Interfaces.Play;

namespace Cadenza.Components.Sidebar;

public class PlayShortcutsBase : ComponentBase
{
    [Parameter]
    public bool IsAppLoaded { get;set;}

    [Inject]
    public IItemPlayer Player { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IHistoryFetcher History { get; set; }

    protected List<Grouping> Groupings { get; set; } = new();

    protected List<RecentAlbum> RecentAlbums { get; set; } = new();

    protected override void OnInitialized()
    {
        Messenger.Subscribe<ConnectorEventArgs>(OnConnectorStatusChanged);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished);

        Groupings = Enum.GetValues<Grouping>()
            .Where(g => g != Grouping.None)
            .OrderBy(g => g.ToString())
            .ToList();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (IsAppLoaded)
        {
            await UpdateRecentAlbums();
        }
    }

    private async Task OnConnectorStatusChanged(object sender, ConnectorEventArgs e)
    {
        if (e.Connector != Connector.Database)
            return;

        if (e.Status != ConnectorStatus.Connected)
            return;

        await UpdateRecentAlbums();
    }

    private async Task OnPlaylistFinished(object sender, PlaylistFinishedEventArgs e)
    {
        await UpdateRecentAlbums();
    }

    protected async Task PlayLibrary()
    {
        await Player.PlayAll();
    }

    protected async Task PlayGrouping(Grouping grouping)
    {
        await Player.PlayGrouping(grouping);
    }

    protected async Task PlayRecentAlbum(RecentAlbum album)
    {
        await Player.PlayAlbum(album.Id.ToString());
    }

    private async Task UpdateRecentAlbums()
    {
        RecentAlbums = await History.GetRecentAlbums(10);
        StateHasChanged();
    }
}
