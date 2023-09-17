using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Components.Sidebar;

public class PlayShortcutsBase : ComponentBase
{
    [Parameter]
    public bool IsAppLoaded { get;set;}

    [Inject]
    public IAdminRepository AdminRepository { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

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

        //TODO - what happens when playlist finishes
        //Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished);

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

    // TODO: Update recently played items when playlist changes (could do on start and end if needed)

    //private async Task OnPlaylistFinished(object sender, PlaylistFinishedEventArgs e)
    //{
    //    await UpdateRecentlyPlayedItems();
    //}

    protected Task PlayLibrary()
    {
        Dispatcher.Dispatch(new PlayAllRequest());
        return Task.CompletedTask;
    }

    protected Task PlayGrouping(Grouping grouping)
    {
        Dispatcher.Dispatch(new PlayGroupingRequest(grouping));
        return Task.CompletedTask;
    }

    protected Task PlayRecentAlbum(RecentAlbum album)
    {
        Dispatcher.Dispatch(new PlayAlbumRequest(album.Id, 0));
        return Task.CompletedTask;
    }

    protected Task PlayRecentTag(string tag)
    {
        Dispatcher.Dispatch(new PlayTagRequest(tag));
        return Task.CompletedTask;
    }

    private async Task UpdateRecentlyPlayedItems()
    {
        RecentAlbums = await History.GetRecentAlbums(10);
        RecentTags = await History.GetRecentTags(10);
        StateHasChanged();
    }
}
