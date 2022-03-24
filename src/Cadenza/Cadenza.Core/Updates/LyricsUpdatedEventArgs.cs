namespace Cadenza.Core.Updates;

public delegate Task LyricsUpdatedEventHandler(object sender, LyricsUpdatedEventArgs e);

public class LyricsUpdatedEventArgs : EventArgs
{
    public LyricsUpdatedEventArgs(TrackUpdate update)
    {
        Update = update;
    }

    public TrackUpdate Update { get; }
}