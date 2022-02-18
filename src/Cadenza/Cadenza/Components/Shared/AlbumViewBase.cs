namespace Cadenza.Components.Shared;

public class AlbumViewBase : ComponentBase
{
    [Inject]
    public IPlaylistPlayer PlaylistPlayer { get; set; }

    [Parameter]
    public Album Model { get; set; }

	protected async Task OnPlayAlbum()
	{
		await PlaylistPlayer.PlayAlbum(Model.Source, Model.Id);
	}
}