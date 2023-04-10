using Cadenza.Web.Common.Interfaces.Play;

namespace Cadenza.Components.Sidebar;

public class PlayShortcutsBase : ComponentBase
{
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

        Groupings = Enum.GetValues<Grouping>()
            .Where(g => g != Grouping.None)
            .OrderBy(g => g.ToString())
            .ToList();
    }

    private async Task OnConnectorStatusChanged(object sender, ConnectorEventArgs e)
    {
        if (e.Connector != Connector.Database)
            return;

        if (e.Status != ConnectorStatus.Connected)
            return;

        RecentAlbums = await History.GetRecentAlbums(5);
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
}
