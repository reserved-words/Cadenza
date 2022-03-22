using Cadenza.Core.Updates;

namespace Cadenza.Core.App;

public class ItemUpdatesHandler : IUpdatesController, IUpdatesConsumer
{
    public event ArtistUpdatedEventHandler ArtistUpdated;

    public async Task UpdateArtist(ArtistUpdate update)
    {
        await ArtistUpdated?.Invoke(this, new ArtistUpdatedEventArgs(update));
    }

    public Task UpdateLyrics(TrackUpdate artist)
    {
        throw new NotImplementedException();
    }
}
