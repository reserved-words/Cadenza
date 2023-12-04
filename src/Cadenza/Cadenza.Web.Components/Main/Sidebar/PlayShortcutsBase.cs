namespace Cadenza.Web.Components.Main.Sidebar;

public class PlayShortcutsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }
    [Inject] public IState<PlaylistHistoryAlbumsState> AlbumHistoryState { get; set; }
    [Inject] public IState<PlaylistHistoryTagsState> TagHistoryState { get; set; }

    protected IReadOnlyCollection<GroupingVM> Groupings => GroupingsState.Value.Groupings;
    protected IReadOnlyCollection<RecentAlbumVM> RecentAlbums => AlbumHistoryState.Value.Items.Take(8).ToList();
    protected IReadOnlyCollection<string> RecentTags => TagHistoryState.Value.Items.Take(8).ToList();

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
