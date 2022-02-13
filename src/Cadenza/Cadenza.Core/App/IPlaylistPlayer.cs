namespace Cadenza.Core;

public interface IPlaylistPlayer
{
    Task PlayArtist(string id);
    Task PlayAlbum(LibrarySource source, string id);
    Task PlayTrack(LibrarySource source, string id);
    Task PlayPlaylist(string id);
    Task PlayAll();
}
