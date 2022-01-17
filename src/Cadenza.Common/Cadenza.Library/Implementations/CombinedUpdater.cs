namespace Cadenza.Library;

public class CombinedUpdater : IUpdater
{
    private readonly IEnumerable<ISourceUpdater> _updaters;

    internal CombinedUpdater(IEnumerable<ISourceUpdater> updaters)
    {
        _updaters = updaters;
    }

    public async Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.Update(album, updates);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.Update(artist, updates);
            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        foreach (var updater in _updaters)
        {
            var success = await updater.Update(track, updates);
            if (!success)
                return false;
        }

        return true;
    }
}
