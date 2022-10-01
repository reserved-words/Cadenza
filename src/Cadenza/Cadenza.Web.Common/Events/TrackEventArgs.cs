namespace Cadenza.Web.Common.Events;

public delegate Task TrackEventHandler(object sender, TrackEventArgs e);

public class TrackEventArgs : EventArgs
{
    public PlayTrack CurrentTrack { get; set; }
    public bool IsLastTrack { get; set; }
}
