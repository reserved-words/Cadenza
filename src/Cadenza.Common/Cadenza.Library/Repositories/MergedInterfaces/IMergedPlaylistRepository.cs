namespace Cadenza.Library;

public interface IMergedPlaylistRepository
{
    Task<Playlist> GetPlaylist(LibrarySource source, string id);
    Task<List<PlaylistTrack>> GetTracks(LibrarySource source, string playlistId);
}