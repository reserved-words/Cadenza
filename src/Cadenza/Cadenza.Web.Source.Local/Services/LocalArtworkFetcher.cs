using Cadenza.Domain.Enums;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Source.Local.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalArtworkFetcher : ISourceArtworkFetcher
{
    private readonly LocalApiSettings _settings;
    private readonly IUrl _url;

    public LocalArtworkFetcher(IOptions<LocalApiSettings> settings, IUrl url)
    {
        _settings = settings.Value;
        _url = url;
    }

    public LibrarySource Source => LibrarySource.Local;

    public Task<string> GetAlbumArtwork(string id)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetTrackArtwork(string id)
    {
        throw new NotImplementedException();
    }
}
