namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    private const int ItemFetchLimit = 500;

    [Inject]
    protected IEnumerable<ISearchRepository> Repositories { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    public bool IsLoading { get; set; } = false;
    public bool IsErrored { get; set; } = false;

    private Dictionary<LibrarySource, List<string>> _sourceArtists = new Dictionary<LibrarySource, List<string>>();

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

    protected SearchableItem Result { get; set; }

    protected Task<IEnumerable<SearchableItem>> Search(string value)
    {
        if (IsCommon(value))
            return null;

        var results = Items.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(results);
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
            foreach (var repository in Repositories)
            {
                await FetchArtists(repository);
                await FetchAlbums(repository);
                await FetchTracks(repository);
            }

            IsLoading = false;
        }
        catch (Exception ex)
        {
            IsLoading = false;
            IsErrored = true;
            Exception = ex;
        }
    }

    private async Task FetchTracks(ISearchRepository repository)
    {
        var response = await repository.GetSearchTracks(1, ItemFetchLimit);
        Items.AddRange(response.Items);

        while (response.Page < response.TotalPages)
        {
            response = await repository.GetSearchTracks(1, ItemFetchLimit);
            Items.AddRange(response.Items);
        }
    }

    private async Task FetchAlbums(ISearchRepository repository)
    {
        var response = await repository.GetSearchAlbums(1, ItemFetchLimit);
        Items.AddRange(response.Items);

        while (response.Page < response.TotalPages)
        {
            response = await repository.GetSearchAlbums(1, ItemFetchLimit);
            Items.AddRange(response.Items);
        }
    }

    private async Task FetchArtists(ISearchRepository repository)
    {
        _sourceArtists.Add(repository.Source, new List<string>());

        var response = await repository.GetSearchArtists(1, ItemFetchLimit);
        AddItems(repository, response);

        while (response.Page < response.TotalPages)
        {
            response = await repository.GetSearchArtists(1, ItemFetchLimit);
            AddItems(repository, response);
        }
    }

    private void AddItems(ISearchRepository repository, ListResponse<SearchableArtist> response)
    {
        foreach (var item in response.Items)
        {
            if (!Items.Any(i => i.Id == item.Id))
            {
                Items.Add(item);
            }
            _sourceArtists[repository.Source].Add(item.Id);
        }
    }

    public Exception Exception = null;
}

