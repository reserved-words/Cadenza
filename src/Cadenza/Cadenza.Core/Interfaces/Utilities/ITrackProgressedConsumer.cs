namespace Cadenza.Core;

public interface ITrackProgressedConsumer
{
    event TrackProgressedEventHandler TrackProgressed;
}