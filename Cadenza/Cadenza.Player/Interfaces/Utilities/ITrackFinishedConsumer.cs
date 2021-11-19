namespace Cadenza.Player;

public interface ITrackFinishedConsumer
{
    event TrackFinishedEventHandler TrackFinished;
}