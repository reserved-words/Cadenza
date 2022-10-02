namespace Cadenza.Web.Common.Interfaces.Play;

public interface IPlayMessenger
{
    event TrackStatusEventHandler TrackStatusChanged;
    event TrackEventHandler StartTrack;

    event PlaylistEventHandler PlaylistFinished;
    event PlaylistEventHandler PlaylistLoading;
    event PlaylistEventHandler PlaylistStarted;
}
