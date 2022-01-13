namespace Cadenza;

public class FavouritePlaylistsBase : ComponentBase
{
    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Parameter]
    public Func<PlaylistDefinition, Task> OnPlay { get; set; }

    public async Task OnShuffleAll()
    {
        var playlist = await PlaylistCreator.CreateLibraryPlaylist();
        await OnPlay(playlist);
    }
}
