namespace Cadenza.Web.Common.Interfaces;

public interface IAppConsumer
{
    event TrackStatusEventHandler TrackStatusChanged;
    event TrackEventHandler StartTrack;

    event PlaylistEventHandler PlaylistFinished;
    event PlaylistEventHandler PlaylistLoading;
    event PlaylistEventHandler PlaylistStarted;

    event ItemEventHandler ItemRequested;
}
