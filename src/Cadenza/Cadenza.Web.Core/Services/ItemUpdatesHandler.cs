using Cadenza.Domain.Model.Update;
using Cadenza.Web.Common.Events;

namespace Cadenza.Web.Core.Services;

internal class ItemUpdatesHandler : IUpdatesController, IUpdatesConsumer
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
