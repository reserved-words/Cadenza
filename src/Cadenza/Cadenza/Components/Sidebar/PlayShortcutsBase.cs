using Cadenza.Web.Common.Interfaces.Play;

namespace Cadenza.Components.Sidebar;

public class PlayShortcutsBase : ComponentBase
{
    [Parameter]
    public bool IsAppLoaded { get;set;}

    [Inject]
    public IAdminRepository AdminRepository { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IHistoryFetcher History { get; set; }

    protected List<Grouping> Groupings { get; set; } = new();
    protected List<RecentAlbum> RecentAlbums { get; set; } = new();
    protected List<string> RecentTags { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Messenger.Subscribe<ConnectorEventArgs>(OnConnectorStatusChanged);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished);

        Groupings = await AdminRepository.GetGroupingOptions();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (IsAppLoaded)
        {
            await UpdateRecentlyPlayedItems();
        }
    }

    private async Task OnConnectorStatusChanged(object sender, ConnectorEventArgs e)
    {
        if (e.Connector != Connector.Database)
            return;

        if (e.Status != ConnectorStatus.Connected)
            return;

        await UpdateRecentlyPlayedItems();
    }

    private async Task OnPlaylistFinished(object sender, PlaylistFinishedEventArgs e)
    {
        await UpdateRecentlyPlayedItems();
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
        await Player.PlayAlbum(album.Id);
    }

    protected async Task PlayRecentTag(string tag)
    {
        await Player.PlayTag(tag);
    }

    private async Task UpdateRecentlyPlayedItems()
    {
        RecentAlbums = await History.GetRecentAlbums(10);
        RecentTags = await History.GetRecentTags(10);
        StateHasChanged();
    }
}
