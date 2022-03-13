using Cadenza.API.Interfaces;
using Cadenza.Library;

namespace Cadenza.API.Services;

public class ApiUpdateService : IApiUpdateService
{
    private readonly ILibrary _library;
    private readonly IArtistCache _artistCache;

    public ApiUpdateService(ILibrary library, IArtistCache artistCache)
    {
        _library = library;
        _artistCache = artistCache;
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var artist = await _artistCache.GetArtist(update.Id);

        if (artist == null)
            return;

        await _artistCache.UpdateArtist(update);
        await _library.UpdateArtist(update);
    }
}
