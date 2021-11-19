namespace Cadenza;

public class LibraryAlbumBase : ComponentBase
{
    [Parameter]
    public AlbumViewModel Model { get; set; }

    [Parameter]
    public Func<AlbumTrackViewModel, Task> OnPlayTrack { get; set; }

    [Parameter]
    public Func<AlbumViewModel, Task> OnPlayAlbum { get; set; }
}