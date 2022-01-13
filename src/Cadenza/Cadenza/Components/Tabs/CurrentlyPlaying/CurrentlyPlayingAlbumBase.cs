namespace Cadenza;

public class CurrentlyPlayingAlbumBase : ComponentBase
{
    [Parameter]
    public FullTrack Track { get; set; }
}