using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalArtworkFetcher : ISourceArtworkFetcher
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly LocalApiSettings _settings;

    public LocalArtworkFetcher(IOptions<LocalApiSettings> settings, IAlbumRepository albumRepository, IArtistRepository artistRepository)
    {
        _settings = settings.Value;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task<string> GetArtistImageUrl(ArtistInfo artist, string trackId)
    {
        if (trackId != null)
            return GetArtistImageUrl(trackId);

        var tracks = await _artistRepository.GetTracks(artist.Id);
        if (!tracks.Any())
            return null;

        return GetArtistImageUrl(tracks.First().Id);
    }

    public async Task<string> GetArtworkUrl(Album album, string trackId)
    {
        if (trackId != null)
            return GetArtworkUrl(trackId);

        var tracks = await _albumRepository.GetTracks(album.Id);
        return GetArtworkUrl(tracks.First().TrackId);
    }

    private string GetArtistImageUrl(string trackId)
    {
        var urlFormat = $"{_settings.BaseUrl}{_settings.Endpoints.ArtistImageUrl}";
        return string.Format(urlFormat, trackId);
    }

    private string GetArtworkUrl(string trackId)
    {
        var urlFormat = $"{_settings.BaseUrl}{_settings.Endpoints.ArtworkUrl}";
        return string.Format(urlFormat, trackId);
    }
}
