using Cadenza.Web.Common.Events;

namespace Cadenza.Web.Common.Interfaces;

public interface IUpdatesConsumer
{
    event ArtistUpdatedEventHandler ArtistUpdated;
    event LyricsUpdatedEventHandler LyricsUpdated;
}
