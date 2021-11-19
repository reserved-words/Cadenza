namespace Cadenza;

public class LibraryArtistBase : ComponentBase
{
    [Inject]
    public IViewModelLibrary Library { get; set; }

    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Inject]
    public INameComparer Comparer { get; set; }

    [Parameter]
    public string ArtistId { get; set; }

    [Parameter]
    public Func<PlaylistDefinition, Task> OnPlay { get; set; }

    public ArtistViewModel Model { get; set; }

    public string PlaceholderText { get; set; }

    protected override void OnInitialized()
    {
        PlaceholderText = "No artist selected";

        Library.AlbumUpdated += OnAlbumUpdated;
        Library.ArtistUpdated += OnArtistUpdated;
    }

    private async Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs e)
    {
        if (Model == null || Model.Artist.Id != e.Update.Id)
            return;

        await UpdateArtist();
    }

    private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        if (Model == null || Model.Artist.Id != e.Update.Id)
            return;

        await UpdateArtist();
    }

    protected override async Task OnParametersSetAsync()
    {
        PlaceholderText = "Loading artist...";

        Model = null;

        if (ArtistId != null)
        {
            await UpdateArtist();
        }

        PlaceholderText = "No artist selected";
    }

    private async Task UpdateArtist()
    {
        Model = await Library.GetArtist(ArtistId);
        StateHasChanged();
    }

    public async Task OnPlayAlbum(AlbumViewModel album)
    {
        var playlist = await PlaylistCreator.CreateAlbumPlaylist(album.Model);
        await OnPlay(playlist);
    }

    public async Task OnPlayArtist(AlbumTrackViewModel track)
    {
        var playlist = await PlaylistCreator.CreateArtistPlaylist(Model.Artist.Id, track.Model.Track.Id);
        await OnPlay(playlist);
    }
}