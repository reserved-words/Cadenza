namespace Cadenza.Web.Common.Events;

public class TrackStatusEventArgs : EventArgs
{
    public PlayTrack Track { get; set; }
    public PlayStatus Status { get; set; }
}

