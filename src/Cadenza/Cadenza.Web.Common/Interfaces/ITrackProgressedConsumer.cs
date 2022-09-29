using Cadenza.Web.Common.Events;

namespace Cadenza.Web.Common.Interfaces;

public interface ITrackProgressedConsumer
{
    event TrackProgressedEventHandler TrackProgressed;
}