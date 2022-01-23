using Cadenza.Common;
using Cadenza.Core;

namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    [Inject]
    protected IMainRepository Repository { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    public bool IsLoading { get; set; } = true;
    public bool IsErrored { get; set; } = false;

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

    protected List<SearchableItem> Items = new List<SearchableItem>();

    protected override async Task OnInitializedAsync()
    {
        App.LibraryUpdated += App_LibraryUpdated;
    }

    private async Task App_LibraryUpdated(object sender, LibraryEventArgs e)
    {
        await Update();
    }

    protected override async Task OnParametersSetAsync()
    {
        await Update();
    }

    protected SearchableItem Result { get; set; }

    protected async Task<IEnumerable<SearchableItem>> Search(string value)
    {
        if (IsCommon(value))
            return null;

        return Items.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private static bool IsCommon(string value)
    {
        return value.Equals("the", StringComparison.InvariantCultureIgnoreCase)
            || value.Equals("the ", StringComparison.InvariantCultureIgnoreCase);
    }

    private async Task Update()
    {
        IsErrored = false;
        IsLoading = true;
        Items.Clear();
        Exception = null;

        try
        {
            var items = await Repository.GetSearchableItems();
            Items = items.ToList();
            IsLoading = false;
        }
        catch (Exception ex)
        {
            IsLoading = false;
            IsErrored = true;
            Exception = ex;
        }
    }

    public Exception Exception = null;
}

