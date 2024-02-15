namespace Cadenza.Web.Components.Features.Tabs.Library.Components;

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
    public IReadOnlyCollection<TaggedItemVM> Items => ViewTagState.Value.Items;

    protected readonly Dictionary<string, PlayerItemType?> FilterTypes = new Dictionary<string, PlayerItemType?>();

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>
    {
        new LibraryBreadcrumb(PlayerItemType.Tag, Tag, Tag)
    };

    protected string FilterType { get; set; }

    protected bool OnFilter(TaggedItemVM item)
    {
        var filterType = FilterTypes[FilterType];
        return !filterType.HasValue || item.Type == filterType;
    }

    protected void OnViewItem(TaggedItemVM item)
    {
        Dispatcher.Dispatch(new ViewItemRequest(item.Type, item.Id.ToString(), item.Name));
    }

    private void AddFilterType(PlayerItemType type)
    {
        FilterTypes.Add(type.ToString(), type);
    }
}
