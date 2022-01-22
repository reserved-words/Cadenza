namespace Cadenza.Components.Sidebar;

public enum SearchableItemType
{
    Artist,
    Album,
    Track
}

public class SearchableItem
{
    public SearchableItemType Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string AdditionalInfo { get; set; }
}

public class SearchBase : ComponentBase
{
    protected bool resetValueOnEmptyText;
    protected bool coerceText;
    protected bool coerceValue;
    protected string value1, value2;

    protected List<SearchableItem> items = new List<SearchableItem>();

    protected SearchableItem Result { get; set; }

    protected async Task<IEnumerable<SearchableItem>> Search(string value)
    {
        return items.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }
}

