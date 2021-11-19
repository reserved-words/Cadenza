namespace Cadenza.Player;

public interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateArtistPlaylist(string artist, string first = null);

    Task<PlaylistDefinition> CreateAlbumPlaylist(AlbumFull album);

    Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null);
}