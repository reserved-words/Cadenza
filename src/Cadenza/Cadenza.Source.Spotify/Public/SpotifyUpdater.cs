namespace Cadenza.Source.Spotify;

public class SpotifyUpdater : ILibraryUpdater
{
    private readonly ILibraryUpdater _cacheUpdater;
    private readonly IOverridesService _overridesService;

    public SpotifyUpdater(ILibraryUpdater updater, IOverridesService overridesService)
    {
        _cacheUpdater = updater;
        _overridesService = overridesService;
    }

    public async Task<bool> UpdateAlbum(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        var success = await _overridesService.AddOverrides(updates);
        if (success)
        {
            await _cacheUpdater.UpdateAlbum(album);
        }
        return success;
    }

    public async Task<bool> UpdateArtist(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        var success = await _overridesService.AddOverrides(updates);
        if (success)
        {
            await _cacheUpdater.UpdateArtist(artist);
        }
        return success;
    }

    public async Task<bool> UpdateTrack(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        var success = await _overridesService.AddOverrides(updates);
        if (success)
        {
            await _cacheUpdater.UpdateTrack(track);
        }
        return success;
    }
}