namespace Cadenza.Web.Common.Events;

public class TrackRemovedEventArgs : EventArgs
{
    public TrackRemovedEventArgs(string trackId)
    {
        TrackId = trackId;
    }

    public string TrackId { get; }
}