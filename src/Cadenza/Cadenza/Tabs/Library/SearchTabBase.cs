using Cadenza.Web.Common.Interfaces.Searchbar;
using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Tabs.Library;

public class SearchTabBase : ComponentBase
{
    private const string AllTypes = "All";

    [Inject]
    public ISearchCache Cache { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    protected readonly Dictionary<string, PlayerItemType?> ItemTypes = new Dictionary<string, PlayerItemType?>();

    public SearchTabBase()
    {
        ItemTypes.Add(AllTypes, null);

        Enum.GetValues<PlayerItemType>()
            .OrderBy(i => i.ToString())
            .ToList()
            .ForEach(i =>
            {
                ItemTypes.Add(i.ToString(), i);
            });

        OnClear();
    }

    protected List<PlayerItem> Results { get; set; } = new List<PlayerItem>();

    protected string SearchText { get; set; }
    protected string SearchType { get; set; }

    protected void OnClear()
    {
        SearchText = "";
        SearchType = AllTypes;
        Results.Clear();
    }

    protected async Task OnSearch()
    {
        var searchType = ItemTypes[SearchType];

        if (string.IsNullOrWhiteSpace(SearchText) && !searchType.HasValue)
            return; // Add error message

        Results = Cache.Items
            .Where(x => (!searchType.HasValue || x.Type == searchType.Value)
                && !string.IsNullOrWhiteSpace(x.Name)    
                && (string.IsNullOrWhiteSpace(SearchText) || x.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)))
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Name)
            .ToList();
    }

    protected async Task OnViewItem(PlayerItem item)
    {
        await Viewer.ViewSearchResult(item);
    }
}
