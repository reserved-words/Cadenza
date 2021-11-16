namespace Cadenza.Source.Spotify;

internal interface ISpotifyLibraryApi
{
    Task<SpotifyApiAlbumsResponse> GetUserAlbums();
    Task<SpotifyApiPlaylistsResponse> GetUserPlaylists();
}