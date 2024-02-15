namespace Cadenza.Web.Components.Features.Tabs.Library.Components;

public class GroupingTabBase : FluxorComponent
{
    [Inject] public IState<ViewGroupingState> ViewGroupingState { get; set; }

    public bool Loading => ViewGroupingState.Value.IsLoading;
    public string Grouping => ViewGroupingState.Value.Grouping;
    public IReadOnlyCollection<string> Genres => ViewGroupingState.Value.Genres;

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>
    {
        new LibraryBreadcrumb(PlayerItemType.Grouping, Grouping, Grouping)
    };
}
