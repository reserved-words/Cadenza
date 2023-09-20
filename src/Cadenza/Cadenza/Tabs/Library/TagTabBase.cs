using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Tabs.Library;

public class TagTabBase : ComponentBase
{
    private const string AllTypes = "All";

    [Inject]
    public ITagRepository Repository { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Parameter]
    public string Id { get; set; }

    protected readonly Dictionary<string, PlayerItemType?> FilterTypes = new Dictionary<string, PlayerItemType?>();

    public TagTabBase()
    {
        FilterTypes.Add(AllTypes, null);
        AddFilterType(PlayerItemType.Artist);
        AddFilterType(PlayerItemType.Album);
        AddFilterType(PlayerItemType.Track);
        FilterType = AllTypes;
    }

    public List<PlayerItem> Items { get; set; } = new();
    protected string FilterType { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await UpdateTag();
    }

    private async Task UpdateTag()
    {
        Items = (await Repository.GetTag(Id))
            .OrderBy(i => i.Type)
            .ThenBy(i => i.Name)
            .ToList();

        StateHasChanged();
    }

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
