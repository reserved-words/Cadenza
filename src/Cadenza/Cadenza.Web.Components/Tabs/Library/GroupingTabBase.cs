using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Components.Tabs.Library;

public class GroupingTabBase : FluxorComponent
{
    [Inject] public IState<ViewGroupingState> ViewGroupingState { get; set; }

    public bool Loading => ViewGroupingState.Value.IsLoading;
    public GroupingVM Grouping => ViewGroupingState.Value.Grouping;
    public IReadOnlyCollection<string> Genres => ViewGroupingState.Value.Genres;
}
