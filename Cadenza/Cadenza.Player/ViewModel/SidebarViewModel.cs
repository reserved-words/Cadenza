namespace Cadenza.Player;

public class SidebarViewModel
{
    public PlayingTrack CurrentTrack { get; set; }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipNext { get; set; }
}