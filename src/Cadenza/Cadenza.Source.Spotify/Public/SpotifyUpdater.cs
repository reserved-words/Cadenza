//namespace Cadenza.Source.Spotify;

//public class SpotifyUpdater : ILibraryUpdater
//{
//    private readonly IUpdater _cacheUpdater;
//    private readonly IOverridesService _overridesService;

//    public SpotifyUpdater(ILibraryUpdater updater, IOverridesService overridesService)
//    {
//        _cacheUpdater = updater;
//        _overridesService = overridesService;
//    }

//    public async Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates)
//    {
//        var success = await _overridesService.AddOverrides(updates);
//        if (success)
//        {
//            await _cacheUpdater.Update(album, updates);
//        }
//        return success;
//    }

//    public async Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates)
//    {
//        var success = await _overridesService.AddOverrides(updates);
//        if (success)
//        {
//            await _cacheUpdater.Update(artist, updates);
//        }
//        return success;
//    }

//    public async Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates)
//    {
//        var success = await _overridesService.AddOverrides(updates);
//        if (success)
//        {
//            await _cacheUpdater.Update(track, updates);
//        }
//        return success;
//    }
//}