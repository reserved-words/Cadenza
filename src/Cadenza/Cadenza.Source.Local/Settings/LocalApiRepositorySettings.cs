using Cadenza.Library.Repositories;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

internal class LocalApiRepositorySettings : IApiRepositorySettings
{
    private readonly LocalApiSettings _settings;

    public LocalApiRepositorySettings(IOptions<LocalApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public string BaseUrl => _settings.BaseUrl;

    public string Artist => _settings.Endpoints.Artist;
    public string ArtistAlbums => _settings.Endpoints.ArtistAlbums;
    public string AllArtists => _settings.Endpoints.AllArtists;
    public string AlbumArtists => _settings.Endpoints.AlbumArtists;
    public string TrackArtists => _settings.Endpoints.TrackArtists;
    public string ArtistsByGrouping => _settings.Endpoints.GroupingArtists;
    public string ArtistsByGenre => _settings.Endpoints.GenreArtists;

    public string PlayTracks => _settings.Endpoints.PlayTracks;
    public string PlayArtist => _settings.Endpoints.PlayArtist;
    public string PlayAlbum => _settings.Endpoints.PlayAlbum;
    public string PlayGenre => _settings.Endpoints.PlayGenre;
    public string PlayGrouping => _settings.Endpoints.PlayGrouping;

    public string SearchArtists => _settings.Endpoints.SearchArtists;
    public string SearchAlbums => _settings.Endpoints.SearchAlbums;
    public string SearchTracks => _settings.Endpoints.SearchTracks;
    public string SearchPlaylists => _settings.Endpoints.SearchPlaylists;
    public string SearchGenres => _settings.Endpoints.SearchGenres;
    public string SearchGroupings => _settings.Endpoints.SearchGroupings;

    public string Track => _settings.Endpoints.Track;
    public string Album => _settings.Endpoints.Album;
    public string AlbumTracks => _settings.Endpoints.AlbumTracks;

    public string UpdateAlbum => _settings.Endpoints.UpdateAlbum;
    public string UpdateArtist => _settings.Endpoints.UpdateArtist;
    public string UpdateTrack => _settings.Endpoints.UpdateTrack;
}


