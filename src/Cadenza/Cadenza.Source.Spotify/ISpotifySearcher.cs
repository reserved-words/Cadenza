using Cadenza.Source.Spotify.Model;

namespace Cadenza.Source.Spotify;

public interface ISpotifySearcher
{
    Task<List<SpotifyArtist>> SearchArtist(string artistName);
    Task<List<SpotifyAlbum>> GetArtistAlbums(string artistId);
    Task<List<SpotifyPlaylist>> GetArtistPlaylists(string artistName);
    Task<List<SpotifyTrack>> GetPlaylistTracks(string playlistId);
    Task<List<SpotifyTrack>> GetAlbumTracks(string albumId);
}
