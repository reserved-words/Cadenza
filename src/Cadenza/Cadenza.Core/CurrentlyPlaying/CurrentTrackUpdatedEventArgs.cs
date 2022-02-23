namespace Cadenza.Core.CurrentlyPlaying;

public class CurrentTrackUpdatedEventArgs : EventArgs
{
    public Track UpdatedTrack { get; }
}
