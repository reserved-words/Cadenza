using Cadenza.Source.Spotify.Api.Model.Albums;
using Cadenza.Source.Spotify.Api.Model.Playlists;

namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface IUserApi
{
    Task<List<SpotifyApiAlbumsItem>> GetUserAlbums();
    Task<List<SpotifyApiPlaylist>> GetUserPlaylists();
    Task AddAlbum(string albumId);
    Task AddPlaylist(string playlistId);

}
