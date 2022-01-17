using Cadenza.Domain;

namespace Cadenza.Library;


// KEEP INTERNAL FOR NOW, MAY BE NEEDED PUBLIC OR CAN BE REMOVED BUT NOT SURE
internal class CombinedSourceLibraryUpdater : ILibraryUpdater
{
    private readonly Dictionary<LibrarySource, ILibraryUpdater> _sourceUpdaters;
    private readonly ICacher _cacheUpdater;

    public CombinedSourceLibraryUpdater(Dictionary<LibrarySource, ILibraryUpdater> sourceUdpaters, IMerger merger, ICache cache)
    {
        _sourceUpdaters = sourceUdpaters;
        _cacheUpdater = new SimpleCacher(merger, cache);
    }

    public async Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        var updater = _sourceUpdaters[album.Source];
        var success = await updater.Update(album, updates);
        if (success)
        {
            _cacheUpdater.AddAlbum(album, true);
        }
        return success;
    }

    public async Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        foreach (var source in _sourceUpdaters.Keys)
        {
            // Artist might not be in source but source responsible for checking that
            // Try to do this asynchronously so can happen at the same time for all sources

            var updater = _sourceUpdaters[source];
            var success = await updater.Update(artist, updates);

            // if successful need to update cache

            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        var updater = _sourceUpdaters[track.Source];
        var success = await updater.Update(track, updates);
        if (success)
        {
            _cacheUpdater.AddTrack(track, true);
        }
        return success;
    }
}