using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Core.Coordinators;

internal class UpdatesCoordinator : IUpdatesController, IUpdatesMessenger
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
