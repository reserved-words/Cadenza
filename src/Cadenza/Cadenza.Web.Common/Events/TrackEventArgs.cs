namespace Cadenza.Web.Common.Events;

public delegate Task TrackEventHandler(object sender, TrackEventArgs e);

public class TrackEventArgs : EventArgs
{
    public PlayTrack CurrentTrack { get; set; }
    //public int PercentagePlayed { get; set; }
    public bool IsLastTrack { get; set; }
    //    public string Error { get; set; }
}

public delegate Task TrackStatusEventHandler(object sender, TrackStatusEventArgs e);

public class TrackStatusEventArgs : EventArgs
{
    public PlayTrack Track { get; set; }
    public PlayStatus Status { get; set; }
}

public enum PlayStatus
{
    Playing, 
    Paused,
    Stopped
}