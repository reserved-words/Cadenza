namespace Cadenza.Core;

public interface IAppConsumer
{
    event TrackEventHandler TrackFinished;
    event TrackEventHandler TrackPaused;
    event TrackEventHandler TrackResumed;
    event TrackEventHandler TrackStarted;

    event PlaylistEventHandler PlaylistFinished;
    event PlaylistEventHandler PlaylistLoading;
    event PlaylistEventHandler PlaylistStarted;

    event LibraryEventHandler LibraryUpdated;
}