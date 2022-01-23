namespace Cadenza.Core;

public interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateArtistPlaylist(string id);

    Task<PlaylistDefinition> CreateAlbumPlaylist(string id);

    Task<PlaylistDefinition> CreateTrackPlaylist(string trackId, string albumId);

    Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null);
}