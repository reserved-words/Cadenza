namespace Cadenza.Library;

public interface IPlaylistRepository
{
    Task<Playlist> GetPlaylist(string id);
    Task<List<PlaylistTrack>> GetPlaylistTracks(string albumId);
}
