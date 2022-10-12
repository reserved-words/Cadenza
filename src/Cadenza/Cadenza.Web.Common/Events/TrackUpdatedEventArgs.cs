namespace Cadenza.Web.Common.Events;

public class TrackUpdatedEventArgs : EventArgs
{
    public TrackUpdatedEventArgs(TrackUpdate update)
    {
        Update = update;
    }

    public TrackUpdate Update { get; }
}
