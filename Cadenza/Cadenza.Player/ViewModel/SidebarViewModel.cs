namespace Cadenza.Player;

public class SidebarViewModel
{
    public TrackSummary CurrentTrack { get; set; }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipNext { get; set; }
}