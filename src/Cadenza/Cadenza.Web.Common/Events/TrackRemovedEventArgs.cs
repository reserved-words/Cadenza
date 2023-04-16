namespace Cadenza.Web.Common.Events;

public class TrackRemovedEventArgs : EventArgs
{
    public TrackRemovedEventArgs(int trackId)
    {
        TrackId = trackId;
    }

    public int TrackId { get; }
}