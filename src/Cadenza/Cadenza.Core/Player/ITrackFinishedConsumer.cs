namespace Cadenza.Core;

public interface ITrackFinishedConsumer
{
    event TrackFinishedEventHandler TrackFinished;
}