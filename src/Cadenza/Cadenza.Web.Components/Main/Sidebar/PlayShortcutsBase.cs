namespace Cadenza.Web.Components.Main.Sidebar;

public class PlayShortcutsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }
    [Inject] public IState<HistoryRecentlyPlayedAlbumsState> AlbumHistoryState { get; set; }

    protected IReadOnlyCollection<string> Groupings => GroupingsState.Value.Groupings;

    protected Task PlayLibrary()
    {
        Dispatcher.Dispatch(new PlayAllRequest());
        return Task.CompletedTask;
    }

    protected Task PlayGrouping(string grouping)
    {
        Dispatcher.Dispatch(new PlayGroupingRequest(grouping));
        return Task.CompletedTask;
    }
}
