namespace Cadenza.Player;

public interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateArtistPlaylist(LibraryArtist artist);

    Task<PlaylistDefinition> CreateAlbumPlaylist(LibraryAlbum album);

    Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null);
}