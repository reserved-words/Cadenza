namespace Cadenza.Web.Core.Player;

public interface ITrackProgressedConsumer
{
    event TrackProgressedEventHandler TrackProgressed;
}