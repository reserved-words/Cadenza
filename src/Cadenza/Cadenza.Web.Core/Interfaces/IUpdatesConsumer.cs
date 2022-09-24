using Cadenza.Web.Core.Updates;

namespace Cadenza.Web.Core.Interfaces;

public interface IUpdatesConsumer
{
    event ArtistUpdatedEventHandler ArtistUpdated;
    event LyricsUpdatedEventHandler LyricsUpdated;
}
