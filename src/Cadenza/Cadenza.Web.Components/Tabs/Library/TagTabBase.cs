using Fluxor;

namespace Cadenza.Web.Components.Tabs.Library;

public class TagTabBase : FluxorComponent
{
    private const string AllTypes = "All";

    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<ViewTagState> ViewTagState { get; set; }

    public TagTabBase()
    {
        FilterTypes.Add(AllTypes, null);
        AddFilterType(PlayerItemType.Artist);
        AddFilterType(PlayerItemType.Album);
        AddFilterType(PlayerItemType.Track);
        FilterType = AllTypes;
    }

    public bool Loading => ViewTagState.Value.IsLoading;
    public string Tag => ViewTagState.Value.Tag;
    public List<PlayerItem> Items => ViewTagState.Value.Items;

    protected readonly Dictionary<string, PlayerItemType?> FilterTypes = new Dictionary<string, PlayerItemType?>();
    protected string FilterType { get; set; }

    protected bool OnFilter(PlayerItem item)
    {
        var filterType = FilterTypes[FilterType];
        return !filterType.HasValue || item.Type == filterType;
    }

    protected void OnViewItem(PlayerItem item)
    {
        Dispatcher.Dispatch(new ViewItemRequest(item.Type, item.Id, item.Name));
    }

    private void AddFilterType(PlayerItemType type)
    {
        FilterTypes.Add(type.ToString(), type);
    }
}
