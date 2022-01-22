﻿namespace Cadenza.Core;

public delegate Task TrackEventHandler(object sender, TrackEventArgs e);

public class TrackEventArgs : EventArgs
{
    public TrackSummary CurrentTrack { get; set; }
    public int PercentagePlayed { get; set; }
    public bool IsLastTrack { get; set; }
}