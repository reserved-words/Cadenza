namespace Cadenza.Player;

public class PlayerLibrary : ILibraryConsumer, ILibraryController
{
    public event AlbumUpdatedEventHandler AlbumUpdated;
    public event ArtistUpdatedEventHandler ArtistUpdated;
    public event TrackUpdatedEventHandler TrackUpdated;

    private readonly ILibraryUpdater _updater;

    public PlayerLibrary(ILibraryUpdater updater)
    {
        _updater = updater;
    }

    public async Task<bool> UpdateAlbum(AlbumUpdate update)
    {
        var success = await _updater.UpdateAlbum(update);
        if (success)
        {
            AlbumUpdated?.Invoke(this, new AlbumUpdatedEventArgs(update.Item.Source, update));
        }
        return success;
    }

    public async Task<bool> UpdateArtist(ArtistUpdate update)
    {
        var success = await _updater.UpdateArtist(update);
        if (success)
        {
            ArtistUpdated?.Invoke(this, new ArtistUpdatedEventArgs(update));
        }
        return success;
        // Artist name not updated atm so don't need to include sidebar but will need to update:
        // Library Artist tab
        // Currently Playing tab if relevant
        // Library artist list - artist may have changed grouping
    }

    public async Task<bool> UpdateTrack(TrackUpdate update)
    {
        var success = await _updater.UpdateTrack(update);
        if (success)
        {
            TrackUpdated?.Invoke(this, new TrackUpdatedEventArgs(update));
        }
        return success;
    }
}