namespace Cadenza;

public class LibraryTabBase : ComponentBase
{
    [Inject]
    public IArtistRepository Repository { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Parameter]
    public Func<PlaylistDefinition, Task> OnPlay { get; set; }

    public Grouping? SelectedGrouping { get; set; }

    public List<Grouping?> Groupings => GetGroupings();

    public bool Loading => Artists == null || !Artists.Any();

    public List<LibraryArtist> Artists { get; set; }

    public LibraryArtist SelectedArtist { get; set; }

    public string SelectedArtistId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        App.LibraryUpdated += App_LibraryUpdated;
    }

    private async Task App_LibraryUpdated(object sender, LibraryEventArgs e)
    {
        await Update();
    }

    private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        var artist = Artists.SingleOrDefault(a => a.Id == e.Update.Id);
        artist.Grouping = e.Update.Grouping;
    }

    private List<Grouping?> GetGroupings()
    {
        var list = new List<Grouping?> { null };
        list.AddRange(Enum.GetValues<Grouping>().Select(g => (Grouping?)g));
        return list;
    }

    private async Task Update()
    {
        var currentlySelected = SelectedArtistId;

        SelectedArtist = null;
        SelectedArtistId = null;

        Artists = await Repository.GetAlbumArtists();

        StateHasChanged();

        if (Artists != null && currentlySelected != null)
        {
            var selected = Artists.SingleOrDefault(a => a.Id == currentlySelected)
                ?? Artists.FirstOrDefault();

            SelectArtist(selected);
        }
    }

    protected void SelectArtist(LibraryArtist artist)
    {
        SelectedArtist = artist;
        SelectedArtistId = artist?.Id;
    }

    protected async Task OnPlayArtist(LibraryArtist artist)
    {
        var playlist = await PlaylistCreator.CreateArtistPlaylist(artist);
        await OnPlay(playlist);
    }

    public string SearchText { get; set; }

    public bool FilterFunc(LibraryArtist element)
    {
        return (SelectedGrouping == null || element.Grouping == SelectedGrouping)
            && (string.IsNullOrEmpty(SearchText) || element.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase));
    }
}