using Cadenza.Web.Core.Events;

namespace Cadenza.Web.Core.Player;

public interface ITrackFinishedConsumer
{
    event TrackFinishedEventHandler TrackFinished;
}