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

    public async Task<bool> UpdateAlbum(AlbumUpdate album)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.UpdateAlbum(album);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> UpdateArtist(ArtistUpdate artist)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.UpdateArtist(artist);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> UpdateTrack(TrackUpdate track)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.UpdateTrack(track);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<FileUpdateQueue> GetQueue()
    {
        return _updateService.Get();
    }

    public async Task Unqueue(MetaDataUpdate update)
    {
        _updateService.Remove(update);
    }
}