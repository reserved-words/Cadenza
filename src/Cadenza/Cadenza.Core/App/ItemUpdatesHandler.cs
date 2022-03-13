using Cadenza.Core.Updates;

namespace Cadenza.Core.App;

public class ItemUpdatesHandler : IUpdatesController, IUpdatesConsumer
{
    public event ArtistUpdatedEventHandler ArtistUpdated;

    public async Task UpdateArtist(ArtistUpdate update)
    {
        update.ApplyUpdates();
        await ArtistUpdated?.Invoke(this, new ArtistUpdatedEventArgs(update.UpdatedItem));
    }
}
