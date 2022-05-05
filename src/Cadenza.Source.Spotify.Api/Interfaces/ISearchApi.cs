using Cadenza.Source.Spotify.Api.Model.Albums;
using Cadenza.Source.Spotify.Api.Model.Artist;
using Cadenza.Source.Spotify.Api.Model.Playlists;

namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface ISearchApi
{
    Task<List<SpotifyApiAlbumTracksItem>> GetAlbumTracks(string albumId);
    Task<List<SpotifyApiPlaylistItem>> GetPlaylistTracks(string playlistId);
    Task<List<SpotifyApiArtist>> SearchArtists(string searchText);
    Task<List<SpotifyApiAlbum>> GetArtistAlbums(string artistId);
    Task<List<SpotifyApiPlaylist>> GetArtistPlaylists(string artistName);
}
