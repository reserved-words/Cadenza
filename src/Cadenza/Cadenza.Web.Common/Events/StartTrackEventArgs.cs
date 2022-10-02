namespace Cadenza.Web.Common.Events;

public class StartTrackEventArgs : EventArgs
{
    public PlayTrack CurrentTrack { get; set; }
    public bool IsLastTrack { get; set; }
}
