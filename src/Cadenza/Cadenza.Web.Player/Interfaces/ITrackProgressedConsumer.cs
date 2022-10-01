using Cadenza.Web.Player.Events;

namespace Cadenza.Web.Player.Interfaces;

internal interface ITrackProgressedConsumer
{
    event TrackProgressedEventHandler TrackProgressed;
}