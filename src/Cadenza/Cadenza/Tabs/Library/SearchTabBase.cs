using Cadenza.Web.Common.Interfaces.Searchbar;
using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Tabs.Library;

public class SearchTabBase : ComponentBase
{
    [Inject]
    public ISearchCache Cache { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    protected List<PlayerItem> Results { get; set; } = new List<PlayerItem>();

    protected async Task OnSearch()
    {
        Results = Cache.Items
            // .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
            .Where(x => x.Type == PlayerItemType.Tag && !string.IsNullOrWhiteSpace(x.Name))
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Name)
            .ToList();
    }

    protected async Task OnViewItem(PlayerItem item)
    {
        await Viewer.ViewSearchResult(item);
    }
}
