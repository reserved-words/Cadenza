using Cadenza.Library;

namespace Cadenza.Local.API;

public class UpdateService : IUpdateService
{
    private readonly ICollection<ILibraryUpdater> _updaters;
    private readonly IFileUpdateService _updateService;

    public UpdateService(ICollection<ILibraryUpdater> updaters, IFileUpdateService updateService)
    {
        _updaters = updaters;
        _updateService = updateService;
    }

    public async Task<bool> UpdateAlbum(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.Update(album, updates);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> UpdateArtist(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.Update(artist, updates);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> UpdateTrack(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.Update(track, updates);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<FileUpdateQueue> GetQueue()
    {
        return _updateService.Get();
    }

    public async Task Unqueue(ItemPropertyUpdate update)
    {
        _updateService.Remove(update);
    }
}