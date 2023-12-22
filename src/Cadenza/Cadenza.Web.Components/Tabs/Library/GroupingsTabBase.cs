namespace Cadenza.Web.Components.Tabs.Library;

public class GroupingsTabBase : FluxorComponent
{
    [Inject] public IState<GroupingsState> State { get; set; }

    public IReadOnlyCollection<string> Groupings => State.Value.Groupings;

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>();
}
