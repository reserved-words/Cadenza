using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Common.Interfaces;

public interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateAlbumPlaylist(int id, int startTrackId);
    Task<PlaylistDefinition> CreateArtistPlaylist(int id);
    Task<PlaylistDefinition> CreateGenrePlaylist(string grouping, string genre);
    Task<PlaylistDefinition> CreateGroupingPlaylist(string grouping);
    Task<PlaylistDefinition> CreateLibraryPlaylist();
    Task<PlaylistDefinition> CreateTagPlaylist(string id);
    Task<PlaylistDefinition> CreateTrackPlaylist(int id);
}