namespace Cadenza.Web.Player;

internal interface ITrackFinishedConsumer
{
    event TrackFinishedEventHandler TrackFinished;
}