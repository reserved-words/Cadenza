using Cadenza.Database;

namespace Cadenza.Player;

public class TrackEventArgs : EventArgs
{
    public PlayingTrack CurrentTrack { get; set; }
    public int PercentagePlayed { get; set; }
    public bool IsLastTrack { get; set; }
}
