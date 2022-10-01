namespace Cadenza.Web.Player;

internal interface ITrackProgressedConsumer
{
    event TrackProgressedEventHandler TrackProgressed;
}