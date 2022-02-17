using Cadenza.Core;

namespace Cadenza;

public class LibraryAlbumBase : ComponentBase
{
    [Inject]
    public IPlaylistPlayer PlaylistPlayer { get; set; }

    //[Parameter]
    //public LibraryAlbum Model { get; set; }

    //protected async Task OnPlayAlbum(LibraryAlbum album)
    //{
    //    await PlaylistPlayer.PlayAlbum(album.Id);
    //}
}