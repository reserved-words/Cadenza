using Cadenza.Core;

namespace Cadenza;

public class LibraryArtistBase : ComponentBase
{
    [Inject]
    public IArtistRepository Repository { get; set; }

    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Parameter]
    public string ArtistId { get; set; }

    [Parameter]
    public Func<PlaylistDefinition, Task> OnPlay { get; set; }

    public LibraryArtistDetails Model { get; set; }

    public string PlaceholderText { get; set; }

    protected override void OnInitialized()
    {
        PlaceholderText = "No artist selected";

        //Repository.AlbumUpdated += OnAlbumUpdated;
        //Repository.ArtistUpdated += OnArtistUpdated;
    }

    private async Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs e)
    {
        if (Model == null || Model.Id != e.Update.Id)
            return;

        await UpdateArtist();
    }

    private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        if (Model == null || Model.Id != e.Update.Id)
            return;

        await UpdateArtist();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (ArtistId == Model?.Id)
            return;

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
        Model = await Repository.GetArtist(ArtistId);
        StateHasChanged();
    }

    public async Task OnPlayAlbum(LibraryAlbum album)
    {
        var playlist = await PlaylistCreator.CreateAlbumPlaylist(album);
        await OnPlay(playlist);
    }
}