﻿namespace Cadenza.Library;

public class CombinedSourceLibraryUpdater : ICombinedSourceLibraryUpdater
{
    private readonly Dictionary<Source, ISourceLibraryUpdater> _sourceUpdaters;
    private readonly ISimpleCacher _cacheUpdater;

    public CombinedSourceLibraryUpdater(Dictionary<Source, ISourceLibraryUpdater> sourceUdpaters, IMerger merger, ICache cache)
    {
        _sourceUpdaters = sourceUdpaters;
        _cacheUpdater = new SimpleCacher(merger, cache);
    }

    public async Task<bool> UpdateAlbum(AlbumUpdate album)
    {
        var updater = _sourceUpdaters[album.Item.Source];
        var success = await updater.UpdateAlbum(album);
        if (success)
        {
            _cacheUpdater.AddAlbum(album.Item, true);
        }
        return success;
    }

    public async Task<bool> UpdateArtist(ArtistUpdate artist)
    {
        foreach (var source in _sourceUpdaters.Keys)
        {
            if (artist.Item.IsInSource(source))
            {
                var updater = _sourceUpdaters[source];
                var success = await updater.UpdateArtist(artist);

                // if successful need to update cache

                if (!success)
                    return false;
            }
        }

        return true;
    }

    public async Task<bool> UpdateTrack(TrackUpdate track)
    {
        var updater = _sourceUpdaters[track.Item.Source];
        var success = await updater.UpdateTrack(track);
        if (success)
        {
            _cacheUpdater.AddTrack(track.Item, true);
        }
        return success;
    }
}