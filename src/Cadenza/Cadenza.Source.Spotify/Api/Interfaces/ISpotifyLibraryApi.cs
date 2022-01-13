namespace Cadenza.Source.Spotify;

public interface ISpotifyLibraryApi
{
    Task<List<SpotifyApiAlbumsItem>> GetUserAlbums();
    Task<List<SpotifyApiPlaylist>> GetUserPlaylists();
    Task<List<SpotifyApiPlaylistItem>> GetPlaylistTracks(string playlistId);
}