namespace Cadenza.Player;

public delegate Task TrackEventHandler(object sender, TrackEventArgs e);

public class TrackEventArgs : EventArgs
{
    public PlayingTrack CurrentTrack { get; set; }
    public int PercentagePlayed { get; set; }
    public bool IsLastTrack { get; set; }
}
