namespace Cadenza.Source.Spotify;

public class SpotifyUpdater : ISourceLibraryUpdater
{
    private readonly ILibraryUpdater _cacheUpdater;
    private readonly IOverridesService _overridesService;

    public SpotifyUpdater(ILibraryUpdater updater, IOverridesService overridesService)
    {
        _cacheUpdater = updater;
        _overridesService = overridesService;
    }

    public async Task<bool> UpdateAlbum(AlbumUpdate album)
    {
        var success = await _overridesService.AddOverrides(album.Updates);
        if (success)
        {
            await _cacheUpdater.UpdateAlbum(album);
        }
        return success;
    }

    public async Task<bool> UpdateArtist(ArtistUpdate artist)
    {
        var success = await _overridesService.AddOverrides(artist.Updates);
        if (success)
        {
            await _cacheUpdater.UpdateArtist(artist);
        }
        return success;
    }

    public async Task<bool> UpdateTrack(TrackUpdate track)
    {
        var success = await _overridesService.AddOverrides(track.Updates);
        if (success)
        {
            await _cacheUpdater.UpdateTrack(track);
        }
        return success;
    }
}