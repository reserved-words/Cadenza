namespace Cadenza.Features.Tabs.Search;

public class SearchTabBase : ComponentBase
{
    private const string AllTypes = "All";

    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<SearchItemsState> SearchItemsState { get; set; }


    protected readonly Dictionary<string, PlayerItemType?> ItemTypes = new Dictionary<string, PlayerItemType?>();

    public SearchTabBase()
    {
        ItemTypes.Add(AllTypes, null);

        Enum.GetValues<PlayerItemType>()
            .Where(i => i != PlayerItemType.Library)
            .OrderBy(i => i.ToString())
            .ToList()
            .ForEach(i =>
            {
                ItemTypes.Add(i.ToString(), i);
            });

        OnClear();
    }

    protected List<SearchItemVM> Results { get; set; } = new List<SearchItemVM>();

    protected string SearchText { get; set; }
    protected string SearchType { get; set; }

    protected void OnClear()
    {
        SearchText = "";
        SearchType = AllTypes;
        Results.Clear();
    }

    protected Task OnSearch()
    {
        var searchType = ItemTypes[SearchType];

        if (string.IsNullOrWhiteSpace(SearchText) && !searchType.HasValue)
            return Task.CompletedTask; // Add error message

        Results = SearchItemsState.Value.Items
            .Where(x => (!searchType.HasValue || x.Type == searchType.Value)
                && !string.IsNullOrWhiteSpace(x.Name)
                && (string.IsNullOrWhiteSpace(SearchText) || x.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)))
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Name)
            .ToList();

        return Task.CompletedTask;
    }
}
