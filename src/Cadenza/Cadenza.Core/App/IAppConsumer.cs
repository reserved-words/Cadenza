namespace Cadenza.Core;

public interface IAppConsumer
{
    event TrackEventHandler TrackStarted;
    event TrackEventHandler TrackPaused;
    event TrackEventHandler TrackResumed;
    event TrackEventHandler TrackFinished;

    event PlaylistEventHandler PlaylistUpdated;

    event LibraryEventHandler LibraryUpdated;
}