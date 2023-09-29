using Fluxor;

namespace Cadenza.Tabs.Library;

public class GroupingTabBase : FluxorComponent
{
    [Inject] public IState<ViewGroupingState> ViewGroupingState { get; set; }

    public bool Loading => ViewGroupingState.Value.IsLoading;
    public Grouping Grouping => ViewGroupingState.Value.Grouping;
    public List<string> Genres => ViewGroupingState.Value.Genres;
}
