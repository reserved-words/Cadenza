using Cadenza.Web.Player.Events;

namespace Cadenza.Web.Player.Interfaces;

internal interface ITrackFinishedConsumer
{
    event TrackFinishedEventHandler TrackFinished;
}