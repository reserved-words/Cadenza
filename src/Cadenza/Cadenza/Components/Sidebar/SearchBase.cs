using Cadenza.Database;

namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    [Inject]
    public SearchRepositoryCache Cache { get; set; }

    public bool IsLoading { get; set; }

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

    protected SourceSearchableItem Result { get; set; }

    protected override void OnInitialized()
    {
        Cache.UpdateStarted += Cache_UpdateStarted;
        Cache.UpdateCompleted += Cache_UpdateCompleted;
    }

    private void Cache_UpdateCompleted(object sender, EventArgs e)
    {
        IsLoading = false;
        StateHasChanged();
    }

    private void Cache_UpdateStarted(object sender, EventArgs e)
    {
        IsLoading = true;
        StateHasChanged();
    }

    protected Task<IEnumerable<SourceSearchableItem>> Search(string value)
    {
        if (IsCommon(value))
            return null;

        var results = Cache.Items.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(results);
    }

    private static bool IsCommon(string value)
    {
        return value.Equals("the", StringComparison.InvariantCultureIgnoreCase)
            || value.Equals("the ", StringComparison.InvariantCultureIgnoreCase);
    }
}

