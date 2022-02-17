using Cadenza.Spotify.API.Model.Albums;
using Cadenza.Spotify.API.Model.Playlists;

namespace Cadenza.Spotify.API.Interfaces;

public interface IApiCaller
{
    Task<List<SpotifyApiAlbumsItem>> GetUserAlbums();
    Task<List<SpotifyApiPlaylist>> GetUserPlaylists();
    Task<List<SpotifyApiPlaylistItem>> GetPlaylistTracks(string playlistId);
}