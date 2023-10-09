namespace Cadenza.API.Core.Services;

internal class WebInfoService : IWebInfoService
{
    private readonly IInfoService _service;

    public WebInfoService(IInfoService service)
    {
        _service = service;
    }

    public async Task<AlbumArtworkDTO> AlbumArtworkUrl(string artist, string title)
    {
        var url = await _service.AlbumArtworkUrl(artist, title);
        return new AlbumArtworkDTO { Url = url };
    }

    public Task<ArtistImageDTO> ArtistImageUrl(string name)
    {
        var result = new ArtistImageDTO { Url = null };
        return Task.FromResult(result);
    }
}
