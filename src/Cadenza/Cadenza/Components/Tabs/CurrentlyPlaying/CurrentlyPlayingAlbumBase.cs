namespace Cadenza;

public class CurrentlyPlayingAlbumBase : ComponentBase
{
    [Parameter]
    public TrackFull Track { get; set; }
}