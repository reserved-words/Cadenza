namespace Cadenza;

public class LibraryAlbumBase : ComponentBase
{
    [Inject]
    public IPlaylistPlayer PlaylistPlayer { get; set; }

    [Parameter]
    public AlbumInfo Model { get; set; }

	protected async Task OnPlayAlbum()
	{
		await PlaylistPlayer.PlayAlbum(Model.Source, Model.Id);
	}
}