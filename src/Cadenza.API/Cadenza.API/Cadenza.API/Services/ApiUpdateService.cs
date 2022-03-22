using Cadenza.API.Interfaces;
using Cadenza.Library;

namespace Cadenza.API.Services;

public class ApiUpdateService : IApiUpdateService
{
    private readonly ILibrary _library;
    private readonly IAlbumCache _albumCache;
    private readonly IArtistCache _artistCache;
    private readonly ITrackCache _trackCache;

    public ApiUpdateService(ILibrary library, IArtistCache artistCache, IAlbumCache albumCache, ITrackCache trackCache)
    {
        _library = library;
        _artistCache = artistCache;
        _albumCache = albumCache;
        _trackCache = trackCache;
    }

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var artist = await _artistCache.GetArtist(update.Id);

        if (artist == null)
            return;

        await _albumCache.UpdateAlbum(update);
        await _library.UpdateAlbum(update);
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var artist = await _artistCache.GetArtist(update.Id);

        if (artist == null)
            return;

        await _artistCache.UpdateArtist(update);
        await _library.UpdateArtist(update);
    }

    public async Task UpdateTrack(TrackUpdate update)
    {
        var track = await _trackCache.GetTrack(update.Id);

        if (track == null)
            return;

        await _trackCache.UpdateTrack(update);
        await _library.UpdateTrack(update);
    }
}
