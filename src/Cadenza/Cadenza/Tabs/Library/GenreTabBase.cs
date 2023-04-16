namespace Cadenza.Tabs.Library;

public class GenreTabBase : ComponentBase, IDisposable
{
    [Inject]
    public IArtistRepository Repository { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Parameter]
    public string Id { get; set; }

    public List<Artist> Artists { get; set; } = new();

    private Guid _updateSubscriptionId = Guid.Empty;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<ArtistUpdatedEventArgs>(OnArtistUpdated, out _updateSubscriptionId);
    }

    private Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        if (!e.Update.IsUpdated(ItemProperty.Genre, out PropertyUpdate genreUpdate))
            return Task.CompletedTask;

        if (genreUpdate.OriginalValue == Id)
        {
            Artists.RemoveWhere(a => a.Id.ToString() == e.Update.Id);
        }

        if (genreUpdate.UpdatedValue == Id)
        {
            Artists = Artists.AddThenSort(e.Update.UpdatedItem, a => a.Name);
        }

        StateHasChanged();

        return Task.CompletedTask;
    }

    protected override async Task OnParametersSetAsync()
    {
        await UpdateGenre();
    }

    private async Task UpdateGenre()
    {
        Artists = (await Repository.GetArtistsByGenre(Id)).OrderBy(a => a.Name).ToList();

        StateHasChanged();
    }

    public void Dispose()
    {
        if (_updateSubscriptionId == Guid.Empty)
            return;

        Messenger.Unsubscribe<ArtistUpdatedEventArgs>(_updateSubscriptionId);
    }
}
