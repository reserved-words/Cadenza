using Cadenza.Database;

namespace Cadenza;

public class LibraryAlbumBase : ComponentBase
{
    [Parameter]
    public LibraryAlbum Model { get; set; }

    [Parameter]
    public Func<LibraryAlbum, Task> OnPlayAlbum { get; set; }
}