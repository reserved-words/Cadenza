using Cadenza.API.Core;
using Cadenza.API.Wrapper.Core;
using Cadenza.Library.Repositories;
using Microsoft.Extensions.Options;

namespace Cadenza.API.Wrapper.Spotify;

public class SpotifyApiSettings : IApiRepositorySettings
{
    private IOptions<ApiSettings> _apiSettings;

    public SpotifyApiSettings(IOptions<ApiSettings> apiSettings)
    {
        _apiSettings = apiSettings;
    }

    public string BaseUrl => _apiSettings.Value.BaseUrl;

    public string Artist => ApiEndpoints.Spotify.Artist;
    public string ArtistAlbums => ApiEndpoints.Spotify.ArtistAlbums;
    public string AllArtists => ApiEndpoints.Spotify.AllArtists;
    public string AlbumArtists => ApiEndpoints.Spotify.AlbumArtists;
    public string TrackArtists => ApiEndpoints.Spotify.TrackArtists;

    public string PlayTracks => ApiEndpoints.Spotify.PlayTracks;
    public string PlayArtist => ApiEndpoints.Spotify.PlayArtist;
    public string PlayAlbum => ApiEndpoints.Spotify.PlayAlbum;

    public string SearchArtists => ApiEndpoints.Spotify.SearchArtists;
    public string SearchAlbums => ApiEndpoints.Spotify.SearchAlbums;
    public string SearchTracks => ApiEndpoints.Spotify.SearchTracks;
    public string SearchPlaylists => ApiEndpoints.Spotify.SearchPlaylists;

    public string Track => ApiEndpoints.Spotify.Track;
    public string Album => ApiEndpoints.Spotify.Album;
    public string AlbumTracks => ApiEndpoints.Spotify.AlbumTracks;
}