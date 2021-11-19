namespace Cadenza.Player;

public delegate Task TrackEventHandler(object sender, TrackEventArgs e);
public delegate Task LibraryEventHandler(object sender, LibraryEventArgs e);
public delegate Task PlaylistEventHandler(object sender, PlaylistEventArgs e);

public interface IAppConsumer
{
    event TrackEventHandler TrackStarted;
    event TrackEventHandler TrackPaused;
    event TrackEventHandler TrackResumed;
    event TrackEventHandler TrackFinished;

    event PlaylistEventHandler PlaylistUpdated;

    IPlaylist CurrentPlaylist { get; }
}