namespace Cadenza.Web.Common.Interfaces;

public interface IPlaylistHistory
{
    Task<List<RecentAlbum>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);
    Task LogPlayedItem(PlaylistId playlistId);
}