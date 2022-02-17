namespace Cadenza.Core;

public interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateArtistPlaylist(string id);

    Task<PlaylistDefinition> CreateAlbumPlaylist(LibrarySource source, string id);

    Task<PlaylistDefinition> CreateTrackPlaylist(LibrarySource source, string id);

    Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null);
}