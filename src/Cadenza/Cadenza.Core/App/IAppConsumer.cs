using Cadenza.Core.CurrentlyPlaying;

namespace Cadenza.Core.App;

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
    event ItemEventHandler ItemRequested;
}