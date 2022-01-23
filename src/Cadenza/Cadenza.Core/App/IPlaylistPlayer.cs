namespace Cadenza.Core;

public interface IPlaylistPlayer
{
    Task PlayArtist(string id);
    Task PlayAlbum(string id);
    Task PlayTrack(string trackId, string albumId);
    Task PlayPlaylist(string id);
    Task PlayAll();
}
