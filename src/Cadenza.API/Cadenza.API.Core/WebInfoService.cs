using Cadenza.Common.Domain.Model.Results;

namespace Cadenza.API.Core;

internal class WebInfoService : IWebInfoService
{
    private readonly IInfoService _service;

    public WebInfoService(IInfoService service)
    {
        _service = service;
    }

    public async Task<AlbumArtworkResult> AlbumArtworkUrl(string artist, string title)
    {
        var url = await _service.AlbumArtworkUrl(artist, title);
        return new AlbumArtworkResult { Url = url };
    }

    public async Task<ArtistImageResult> ArtistImageUrl(string name)
    {
        var url = await _service.ArtistImageUrl(name);
        return new ArtistImageResult { Url = url };
    }
}
