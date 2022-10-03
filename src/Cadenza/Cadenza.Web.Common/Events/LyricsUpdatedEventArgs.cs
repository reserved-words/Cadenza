namespace Cadenza.Web.Common.Events;

public class LyricsUpdatedEventArgs : EventArgs
{
    public LyricsUpdatedEventArgs(TrackUpdate update)
    {
        Update = update;
    }

    public TrackUpdate Update { get; }
}