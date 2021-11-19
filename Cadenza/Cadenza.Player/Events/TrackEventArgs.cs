namespace Cadenza.Player;

public class TrackEventArgs : EventArgs
{
    public PlaylistTrackViewModel CurrentTrack { get; set; }
    public int PercentagePlayed { get; set; }
    public bool IsLastTrack { get; set; }
}
