namespace Cadenza.Web.Common.Interfaces;

public interface IAppConsumer
{
    event TrackEventHandler TrackFinished;
    event TrackEventHandler TrackPaused;
    event TrackEventHandler TrackResumed;
    event TrackEventHandler TrackStarted;
    event TrackEventHandler StartTrack;

    event PlaylistEventHandler PlaylistFinished;
    event PlaylistEventHandler PlaylistLoading;
    event PlaylistEventHandler PlaylistStarted;

    event ItemEventHandler ItemRequested;
}
