namespace Cadenza.Web.Components.Main.Sidebar;

public class PlayShortcutsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }
    [Inject] public IState<PlaylistHistoryAlbumsState> AlbumHistoryState { get; set; }
    [Inject] public IState<PlaylistHistoryTagsState> TagHistoryState { get; set; }

    protected List<GroupingVM> Groupings => GroupingsState.Value.Groupings;
    protected List<RecentAlbumVM> RecentAlbums => AlbumHistoryState.Value.Items;
    protected List<string> RecentTags => TagHistoryState.Value.Items;

    protected Task PlayLibrary()
    {
        Dispatcher.Dispatch(new PlayAllRequest());
        return Task.CompletedTask;
    }

    protected Task PlayGrouping(GroupingVM grouping)
    {
        Dispatcher.Dispatch(new PlayGroupingRequest(grouping));
        return Task.CompletedTask;
    }

    protected Task PlayRecentAlbum(RecentAlbumVM album)
    {
        Dispatcher.Dispatch(new PlayAlbumRequest(album.Id, 0));
        return Task.CompletedTask;
    }

    protected Task PlayRecentTag(string tag)
    {
        Dispatcher.Dispatch(new PlayTagRequest(tag));
        return Task.CompletedTask;
    }
}
