namespace Cadenza.Features.Tabs.Library.Components;

public class GroupingsTabBase : FluxorComponent
{
    [Inject] public IState<GroupingsState> State { get; set; }

    public IReadOnlyCollection<string> Groupings => State.Value.Groupings;

    protected List<LibraryBreadcrumbItem> Breadcrumbs => new List<LibraryBreadcrumbItem>();
}
