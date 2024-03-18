namespace Cadenza.Features.Tabs.Library.Components;

public class GroupingTabBase : FluxorComponent
{
    [Inject] public IState<ViewGroupingState> ViewGroupingState { get; set; }

    public bool Loading => ViewGroupingState.Value.IsLoading;
    public string Grouping => ViewGroupingState.Value.Grouping;
    public IReadOnlyCollection<string> Genres => ViewGroupingState.Value.Genres;

    protected List<LibraryBreadcrumbItem> Breadcrumbs => new List<LibraryBreadcrumbItem>
    {
        new LibraryBreadcrumbItem(PlayerItemType.Grouping, Grouping, Grouping)
    };
}
