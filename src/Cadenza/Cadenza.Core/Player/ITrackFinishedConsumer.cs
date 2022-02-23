namespace Cadenza.Core.Player;

public interface ITrackFinishedConsumer
{
    event TrackFinishedEventHandler TrackFinished;
}