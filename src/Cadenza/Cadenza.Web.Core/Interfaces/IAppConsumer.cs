using Cadenza.Web.Core.App;
using Cadenza.Web.Core.CurrentlyPlaying;

namespace Cadenza.Web.Core.Interfaces;

public interface IAppConsumer
{
    event TrackEventHandler TrackFinished;
    event TrackEventHandler TrackPaused;
    event TrackEventHandler TrackResumed;
    event TrackEventHandler TrackStarted;

    event PlaylistEventHandler PlaylistFinished;
    event PlaylistEventHandler PlaylistLoading;
    event PlaylistEventHandler PlaylistStarted;

    event ItemEventHandler ItemRequested;
}
