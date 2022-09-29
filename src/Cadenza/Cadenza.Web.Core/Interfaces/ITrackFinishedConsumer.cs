namespace Cadenza.Web.Core.Interfaces;

internal interface ITrackFinishedConsumer
{
    event TrackFinishedEventHandler TrackFinished;
}