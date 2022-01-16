using Cadenza.Domain;

namespace Cadenza.Library;

public class CombinedSourceLibraryUpdater : ICombinedSourceLibraryUpdater
{
    private readonly Dictionary<LibrarySource, ISourceLibraryUpdater> _sourceUpdaters;
    private readonly ICacher _cacheUpdater;

    public CombinedSourceLibraryUpdater(Dictionary<LibrarySource, ISourceLibraryUpdater> sourceUdpaters, IMerger merger, ICache cache)
    {
        _sourceUpdaters = sourceUdpaters;
        _cacheUpdater = new SimpleCacher(merger, cache);
    }

    public async Task<bool> UpdateAlbum(AlbumInfo album)
    {
        var updater = _sourceUpdaters[album.Source];
        var success = await updater.UpdateAlbum(album);
        if (success)
        {
            _cacheUpdater.AddAlbum(album, true);
        }
        return success;
    }

    public async Task<bool> UpdateArtist(ArtistInfo artist)
    {
        foreach (var source in _sourceUpdaters.Keys)
        {
            // Artist might not be in source but source responsible for checking that
            // Try to do this asynchronously so can happen at the same time for all sources

            var updater = _sourceUpdaters[source];
            var success = await updater.UpdateArtist(artist);

            // if successful need to update cache

            if (!success)
                return false;
        }

        return true;
    }

    public async Task<bool> UpdateTrack(TrackInfo track)
    {
        var updater = _sourceUpdaters[track.Source];
        var success = await updater.UpdateTrack(track);
        if (success)
        {
            _cacheUpdater.AddTrack(track, true);
        }
        return success;
    }
}