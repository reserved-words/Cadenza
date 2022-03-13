using Cadenza.Core.Updates;

namespace Cadenza.Core.App;

public class ItemUpdatesHandler : IUpdatesController, IUpdatesConsumer
{
    public event ArtistUpdatedEventHandler ArtistUpdated;

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var updatedProperties = update.GetUpdates()
            .Select(u => u.Property)
            .ToList();

        update.ApplyUpdates();

        await ArtistUpdated?.Invoke(this, new ArtistUpdatedEventArgs(update.UpdatedItem, updatedProperties));
    }
}
