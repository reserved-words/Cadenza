using Cadenza.Library.Repositories;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Spotify;

public class SpotifyApiRepositorySettings : IApiRepositorySettings
{
    private readonly SpotifyApiSettings _settings;

    public SpotifyApiRepositorySettings(IOptions<SpotifyApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public string BaseUrl => _settings.BaseUrl;

    public string Artist => _settings.Endpoints.Artist;
    public string ArtistAlbums => _settings.Endpoints.ArtistAlbums;
    public string AllArtists => _settings.Endpoints.AllArtists;
    public string AlbumArtists => _settings.Endpoints.AlbumArtists;
    public string TrackArtists => _settings.Endpoints.TrackArtists;

    public string PlayTracks => _settings.Endpoints.PlayTracks;
    public string PlayArtist => _settings.Endpoints.PlayArtist;
    public string PlayAlbum => _settings.Endpoints.PlayAlbum;

    public string SearchArtists => _settings.Endpoints.SearchArtists;
    public string SearchAlbums => _settings.Endpoints.SearchAlbums;
    public string SearchTracks => _settings.Endpoints.SearchTracks;
    public string SearchPlaylists => _settings.Endpoints.SearchPlaylists;

    public string Track => _settings.Endpoints.Track;
    public string Album => _settings.Endpoints.Album;
    public string AlbumTracks => _settings.Endpoints.AlbumTracks;
}
