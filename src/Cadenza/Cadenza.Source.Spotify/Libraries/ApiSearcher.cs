using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Model.Albums;
using Cadenza.Source.Spotify.Model;

namespace Cadenza.Source.Spotify.Libraries;

public class ApiSearcher : ISpotifySearcher
{
    private readonly ISearchApi _api;

    public ApiSearcher(ISearchApi api)
    {
        _api = api;
    }

    public async Task<List<SpotifyArtist>> SearchArtist(string artistName)
    {
        var searchResults = await _api.SearchArtists(artistName);
        return searchResults
            .Select(a => new SpotifyArtist
            {
                Id = a.id,
                Name = a.name
            })
            .ToList();
    }

    public async Task<List<SpotifyAlbum>> GetArtistAlbums(string artistId)
    {
        var searchResults = await _api.GetArtistAlbums(artistId);
        return searchResults
            .Select(a => new SpotifyAlbum
            {
                Id = a.id,
                Title = a.name,
                Artist = a.artists.First().name,
                Year = GetReleaseYear(a),
                ArtworkUrl = a.images.FirstOrDefault()?.url
            })
            .ToList();
    }

    public async Task<List<SpotifyPlaylist>> GetArtistPlaylists(string artistName)
    {
        var searchResults = await _api.GetArtistPlaylists(artistName);
        return searchResults
            .Select(a => new SpotifyPlaylist
            {
                Id = a.id,
                Title = a.name,
                CreatedBy = a.owner.display_name,
                ArtworkUrl = a.images.FirstOrDefault()?.url
            })
            .ToList();
    }

    private string GetReleaseYear(SpotifyApiAlbum album)
    {
        return DateTime.TryParse(album.release_date, out DateTime result)
            ? result.Year.ToString()
            : "";
    }

    public async Task<List<SpotifyTrack>> GetPlaylistTracks(string playlistId)
    {
        var tracks = await _api.GetPlaylistTracks(playlistId);
        return tracks.Select(t => new SpotifyTrack
        {
            Id = t.track.id,
            Title = t.track.name,
            Artist = t.track.artists.First().name,
            Duration = t.track.duration_ms/1000,
            TrackNo = t.track.track_number
        })
        .ToList();
    }

    public async Task<List<SpotifyTrack>> GetAlbumTracks(string albumId)
    {
        var tracks = await _api.GetAlbumTracks(albumId);
        return tracks.Select(t => new SpotifyTrack
        {
            Id = t.id,
            Title = t.name,
            Artist = t.artists.First().name,
            Duration = t.duration_ms / 1000,
            DiscNo = t.disc_number,
            TrackNo = t.track_number
        })
        .ToList();
    }
}
