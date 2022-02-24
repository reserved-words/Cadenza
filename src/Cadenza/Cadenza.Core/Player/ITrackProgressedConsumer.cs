namespace Cadenza.Core.Player;

public interface ITrackProgressedConsumer
{
    event TrackProgressedEventHandler TrackProgressed;
}