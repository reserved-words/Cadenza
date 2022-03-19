using Cadenza.Core.Updates;

namespace Cadenza.Core.App;

public interface IUpdatesConsumer
{
    event ArtistUpdatedEventHandler ArtistUpdated;
}
