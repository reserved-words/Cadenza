namespace Cadenza.Web.Core.Interfaces;

internal interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateArtistPlaylist(string id);

    Task<PlaylistDefinition> CreateAlbumPlaylist(string id);

    Task<PlaylistDefinition> CreateTrackPlaylist(string id);

    Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null);

    Task<PlaylistDefinition> CreateGroupingPlaylist(Grouping id);

    Task<PlaylistDefinition> CreateGenrePlaylist(string id);
}