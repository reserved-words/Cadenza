﻿namespace Cadenza.Source.Spotify;

public interface ISpotifyLibraryApi
{
    Task<SpotifyApiAlbumsResponse> GetUserAlbums();
    Task<SpotifyApiPlaylistsResponse> GetUserPlaylists();
}