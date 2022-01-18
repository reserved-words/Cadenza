namespace Cadenza.Core;

public delegate Task TrackUpdatedEventHandler(object sender, TrackUpdatedEventArgs e);

public class TrackUpdatedEventArgs : EventArgs
{
    public TrackUpdatedEventArgs(TrackUpdate update)
    {
        Update = update;
    }

    public TrackUpdate Update { get; set; }
}