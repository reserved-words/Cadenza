namespace Cadenza;

public class LibraryTabBase : ComponentBase
{
    [Inject]
    public IViewModelLibrary Library { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Parameter]
    public Func<PlaylistDefinition, Task> OnPlay { get; set; }

    [Parameter]
    public Func<List<LibrarySource>, Task> OnSourcesUpdated { get; set; }

    public Grouping? SelectedGrouping { get; set; }

    public List<Grouping?> Groupings => GetGroupings();

    public bool Loading => Artists == null || !Artists.Any();

    public List<Artist> Artists { get; set; }

    public Artist SelectedArtist { get; set; }

    public string SelectedArtistId { get; set; }

    public List<LibrarySource> EnabledSources { get; set; } = new List<LibrarySource>();

    protected override async Task OnInitializedAsync()
    {
        Library.ArtistUpdated += OnArtistUpdated;

        await Update();
    }

    private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        var artist = Artists.SingleOrDefault(a => a.Id == e.Update.Id);
        artist.Grouping = e.Update.Grouping;
        artist.Genre = e.Update.Genre;
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

        EnabledSources = await Library.GetEnabledSources();

        SelectedArtist = null;
        SelectedArtistId = null;

        Artists = await Library.GetAlbumArtists();

        if (Artists != null && currentlySelected != null)
        {
            var selected = Artists.SingleOrDefault(a => a.Id == currentlySelected)
                ?? Artists.FirstOrDefault();

            SelectArtist(selected);
        }
    }

    protected void SelectArtist(Artist artist)
    {
        SelectedArtist = artist;
        SelectedArtistId = artist?.Id;
    }

    protected async Task OnPlayArtist(string artistId)
    {
        var playlist = await PlaylistCreator.CreateArtistPlaylist(artistId);
        await OnPlay(playlist);
    }

    public async Task OnSetSource(LibrarySource source, bool enable)
    {
        if (enable)
        {
            EnabledSources.Add(source);
        }
        else
        {
            EnabledSources.Remove(source);
        }

        await OnSourcesUpdated(EnabledSources);
        await Update();
        StateHasChanged();
    }

    public string SearchText { get; set; }

    public bool FilterFunc(Artist element)
    {
        return (SelectedGrouping == null || element.Grouping == SelectedGrouping)
            && (string.IsNullOrEmpty(SearchText) || element.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase));
    }
}