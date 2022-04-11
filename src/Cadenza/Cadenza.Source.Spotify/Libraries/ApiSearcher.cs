using Cadenza.Core;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Model.Albums;

namespace Cadenza.Source.Spotify.Libraries;

// TODO : Loads of tidying up to do within Spotify project, converting to Cadenza model items etc

public class ApiSearcher : ISpotifySearcher
{
    private readonly IApiCaller _api;

    public ApiSearcher(IApiCaller api)
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
}
