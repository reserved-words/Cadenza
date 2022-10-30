using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Tabs.Library;

public class TagTabBase : ComponentBase
{
    private const string AllTypes = "All";

    [Inject]
    public ITagRepository Repository { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

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

    protected async Task OnViewItem(PlayerItem item)
    {
        await Viewer.ViewSearchResult(item);
    }

    private void AddFilterType(PlayerItemType type)
    {
        FilterTypes.Add(type.ToString(), type);
    }
}
