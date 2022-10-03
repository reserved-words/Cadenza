namespace Cadenza.Web.Common.Events;

public class StopTrackEventArgs : EventArgs
{
    public PlayTrack CurrentTrack { get; set; }
}
