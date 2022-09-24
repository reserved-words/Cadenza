using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Track;
using Cadenza.Library;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Source.Local.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalArtworkFetcher : ISourceArtworkFetcher
{
    private readonly IAlbumRepository _repository;
    private readonly LocalApiSettings _settings;

    public LocalArtworkFetcher(IOptions<LocalApiSettings> settings, IAlbumRepository repository)
    {
        _settings = settings.Value;
        _repository = repository;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task<string> GetArtworkUrl(Album album, string trackId)
    {
        if (trackId != null)
            return GetArtworkUrl(trackId);

        var tracks = await _repository.GetTracks(album.Id);
        if (!tracks.Any())
            return null;

        return GetArtworkUrl(tracks.First().TrackId);
    }

    private string GetArtworkUrl(string trackId)
    {
        var urlFormat = $"{_settings.BaseUrl}{_settings.Endpoints.ArtworkUrl}";
        return string.Format(urlFormat, trackId);
    }
}
