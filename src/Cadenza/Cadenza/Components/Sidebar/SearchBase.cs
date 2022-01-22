namespace Cadenza.Components.Sidebar;

public enum SearchableItemType
{
    Artist,
    Album,
    Track,
    Playlist
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
    protected static Dictionary<SearchableItemType, string> Icons = new Dictionary<SearchableItemType, string>
    {
        { SearchableItemType.Artist, MudBlazor.Icons.Material.Filled.PeopleAlt },
        { SearchableItemType.Album, MudBlazor.Icons.Material.Filled.Album },
        { SearchableItemType.Playlist, MudBlazor.Icons.Material.Filled.QueueMusic },
        { SearchableItemType.Track, MudBlazor.Icons.Material.Filled.MusicNote }
    };

    protected static Dictionary<SearchableItemType, Color> Colors = new Dictionary<SearchableItemType, Color>
    {
        { SearchableItemType.Artist, Color.Primary },
        { SearchableItemType.Album, Color.Secondary },
        { SearchableItemType.Playlist, Color.Info },
        { SearchableItemType.Track, Color.Success }    
    };

    protected List<SearchableItem> items = new List<SearchableItem> 
    { 
        new SearchableItem { Id = "1", Name = "Arch Enemy", Type = SearchableItemType.Artist } 
    };

    protected SearchableItem Result { get; set; }

    protected async Task<IEnumerable<SearchableItem>> Search(string value)
    {
        return items.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }
}

