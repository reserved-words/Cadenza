namespace Cadenza.Web.Common.Events;

public delegate Task TrackStatusEventHandler(object sender, TrackStatusEventArgs e);

public class TrackStatusEventArgs : EventArgs
{
    public PlayTrack Track { get; set; }
    public PlayStatus Status { get; set; }
}

