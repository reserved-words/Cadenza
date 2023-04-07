namespace Cadenza.Tabs.Library;

public class GroupingTabBase : ComponentBase, IDisposable
{
    [Inject]
    public IArtistRepository Repository { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Parameter]
    public string Id { get; set; }

    public Grouping Grouping => Id.Parse<Grouping>();

    public List<string> Genres { get; set; } = new();

    private Dictionary<string, List<Artist>> _artistsByGenre = new();

    private Guid _updateSubscriptionId = Guid.Empty;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<ArtistUpdatedEventArgs>(OnArtistUpdated, out _updateSubscriptionId);
    }

    protected override async Task OnParametersSetAsync()
    {
        await UpdateGrouping();
    }

    private async Task UpdateGrouping()
    {
        var artists = await Repository.GetArtistsByGrouping(Id.Parse<Grouping>());
        _artistsByGenre = artists.ToGroupedDictionary(a => a.Genre ?? "None");
        Genres = _artistsByGenre.Keys.ToList();
        StateHasChanged();
    }

    private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        var isGroupingUpdated = e.Update.IsUpdated(ItemProperty.Grouping);
        var isGenreUpdated = e.Update.IsUpdated(ItemProperty.Genre);

        if (!isGroupingUpdated && !isGenreUpdated)
            return;

        await UpdateGrouping();
    }

    public void Dispose()
    {
        if (_updateSubscriptionId == Guid.Empty)
            return;

        Messenger.Unsubscribe<ArtistUpdatedEventArgs>(_updateSubscriptionId);
    }
}
