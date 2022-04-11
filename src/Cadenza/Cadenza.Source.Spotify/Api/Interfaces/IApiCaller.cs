using Cadenza.Source.Spotify.Api.Model.Albums;
using Cadenza.Source.Spotify.Api.Model.Common;
using Cadenza.Source.Spotify.Api.Model.Playlists;

namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface IApiCaller
{
    Task<List<SpotifyApiAlbumsItem>> GetUserAlbums();
    Task<List<SpotifyApiPlaylist>> GetUserPlaylists();
    Task<List<SpotifyApiPlaylistItem>> GetPlaylistTracks(string playlistId);
    Task<List<SpotifyApiArtist>> SearchArtists(string searchText);
    Task<List<SpotifyApiAlbum>> GetArtistAlbums(string artistId);
    Task<List<SpotifyApiPlaylist>> GetArtistPlaylists(string artistName);
    Task AddAlbum(string albumId);  
    Task AddPlaylist(string playlistId);
}