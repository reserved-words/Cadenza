using Cadenza.Domain.Model.Update;
using Cadenza.Web.Core.Interfaces;
using Cadenza.Web.Core.Updates;

namespace Cadenza.Web.Core.App;

public class ItemUpdatesHandler : IUpdatesController, IUpdatesConsumer
{
    public event ArtistUpdatedEventHandler ArtistUpdated;
    public event LyricsUpdatedEventHandler LyricsUpdated;

    public async Task UpdateArtist(ArtistUpdate update)
    {
        await ArtistUpdated?.Invoke(this, new ArtistUpdatedEventArgs(update));
    }

    public async Task UpdateLyrics(TrackUpdate update)
    {
        await LyricsUpdated?.Invoke(this, new LyricsUpdatedEventArgs(update));
    }
}
