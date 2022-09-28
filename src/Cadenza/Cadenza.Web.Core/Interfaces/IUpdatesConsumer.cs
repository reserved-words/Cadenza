using Cadenza.Web.Core.Events;

namespace Cadenza.Web.Core.Interfaces;

public interface IUpdatesConsumer
{
    event ArtistUpdatedEventHandler ArtistUpdated;
    event LyricsUpdatedEventHandler LyricsUpdated;
}
